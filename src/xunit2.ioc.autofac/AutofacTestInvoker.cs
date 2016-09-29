using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Xunit.Abstractions;
using Xunit.Ioc.Autofac.TestFramework;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac
{
    public class AutofacTestInvoker : TestInvoker<AutofacTestCase>
    {
        public const string TestLifetimeScopeTag = "TestLifetime";

        public AutofacTestInvoker(ILifetimeScope container, ITest test, IMessageBus messageBus, Type testClass, object[] constructorArguments, MethodInfo testMethod, object[] testMethodArguments, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource) 
            : base(test, messageBus, testClass, constructorArguments, testMethod, testMethodArguments, aggregator, cancellationTokenSource)
        {
            _lifetimeScope = container.BeginLifetimeScope(TestLifetimeScopeTag);
            _testOutputHelper = _lifetimeScope.Resolve<TestOutputHelper>();
        }

        public string Output { get; set; }

        protected override Task BeforeTestMethodInvokedAsync()
        {
            _testOutputHelper?.Initialize(MessageBus, Test);

            return base.BeforeTestMethodInvokedAsync();
        }

        protected override Task AfterTestMethodInvokedAsync()
        {
            if (_testOutputHelper != null)
            {
                Output = _testOutputHelper.Output;
                _testOutputHelper.Uninitialize();
            }

            _lifetimeScope?.Dispose();
            return base.AfterTestMethodInvokedAsync();
        }

        protected override object CreateTestClass()
        {
            return _lifetimeScope.Resolve(TestClass);
        }

        private readonly ILifetimeScope _lifetimeScope;
        private readonly TestOutputHelper _testOutputHelper;
    }
}