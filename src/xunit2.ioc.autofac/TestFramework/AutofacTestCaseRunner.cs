using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestCaseRunner : TestCaseRunner<AutofacTestCase>
    {
        public AutofacTestCaseRunner(AutofacTestCase testCase, IContainer container, IMessageBus messageBus, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource, string displayName) 
            : base(testCase, messageBus, aggregator, cancellationTokenSource)
        {
            _container = container;
            _displayName = displayName;
        }

        protected override Task<RunSummary> RunTestAsync()
        {
            var testClass = TestCase.TestMethod.TestClass.Class.ToRuntimeType();
            var testMethod = TestCase.TestMethod.Method.ToRuntimeMethod();
            var test = _container.Resolve<IAutofacTestFactory>().Create(TestCase, _displayName);

            return _container.Resolve<IAutofacTestRunnerFactory>().Create(_container, test, MessageBus, testClass, null, testMethod, null, "", Aggregator, CancellationTokenSource)
                .RunAsync();
        }

        private readonly IContainer _container;
        private readonly string _displayName;
    }
}
