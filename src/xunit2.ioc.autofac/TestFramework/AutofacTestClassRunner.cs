using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestClassRunner : TestClassRunner<AutofacTestCase>
    {
        private readonly IContainer _container;

        public AutofacTestClassRunner(IContainer container, ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<AutofacTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageBus messageBus, ITestCaseOrderer testCaseOrderer, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
            : base(testClass, @class, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource)
        {
            _container = container;
        }

        protected override Task<RunSummary> RunTestMethodAsync(ITestMethod testMethod, IReflectionMethodInfo method, IEnumerable<AutofacTestCase> testCases, object[] constructorArguments)
        {
            return _container.Resolve<IAutofacTestMethodRunnerFactory>().Create(_container, DiagnosticMessageSink, testMethod, Class, method, testCases, MessageBus, new ExceptionAggregator(Aggregator), CancellationTokenSource)
                .RunAsync();
        }

        protected override async Task<RunSummary> RunTestMethodsAsync()
        {
            var summary = new RunSummary();
            IEnumerable<AutofacTestCase> orderedTestCases;
            try
            {
                orderedTestCases = TestCaseOrderer.OrderTestCases(TestCases);
            }
            catch (Exception ex)
            {
                var innerEx = Unwrap(ex);
                DiagnosticMessageSink.OnMessage(new DiagnosticMessage($"Test case orderer '{TestCaseOrderer.GetType().FullName}' threw '{innerEx.GetType().FullName}' during ordering: {innerEx.Message}{Environment.NewLine}{innerEx.StackTrace}"));
                orderedTestCases = TestCases.ToList();
            }


            foreach (var method in orderedTestCases.GroupBy(tc => tc.TestMethod, TestMethodComparer.Instance))
            {
                summary.Aggregate(await RunTestMethodAsync(method.Key, (IReflectionMethodInfo)method.Key.Method, method, null));
                if (CancellationTokenSource.IsCancellationRequested)
                    break;
            }

            return summary;
        }

        public static Exception Unwrap(Exception ex)
        {
            while (true)
            {
                var tiex = ex as TargetInvocationException;
                if (tiex == null)
                    return ex;

                ex = tiex.InnerException;
            }

        }
    }
}
