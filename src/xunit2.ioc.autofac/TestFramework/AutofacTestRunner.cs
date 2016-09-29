using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestRunner : TestRunner<AutofacTestCase>
    {
        public AutofacTestRunner(IContainer container, ITest test, IMessageBus messageBus, Type testClass, object[] constructorArguments, MethodInfo testMethod, object[] testMethodArguments, string skipReason, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource) 
            : base(test, messageBus, testClass, constructorArguments, testMethod, testMethodArguments, skipReason, aggregator, cancellationTokenSource)
        {
            _container = container;
        }

        protected override async Task<Tuple<decimal, string>> InvokeTestAsync(ExceptionAggregator aggregator)
        {
            var invoker = new AutofacTestInvoker(_container, Test, MessageBus, TestClass, null, TestMethod, null, aggregator, CancellationTokenSource);
            var duration = await invoker.RunAsync();

            return Tuple.Create(duration, invoker.Output);
        }

        private readonly IContainer _container;
    }
}