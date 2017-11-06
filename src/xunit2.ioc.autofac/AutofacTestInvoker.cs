using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac
{
    public class AutofacTestInvoker : XunitTestInvoker
    {
        private readonly ILifetimeScope _testLifetimeScope;

        public AutofacTestInvoker(ILifetimeScope testLifetimeScope,
                                  ITest test,
                                  IMessageBus messageBus,
                                  Type testClass,
                                  object[] constructorArguments,
                                  MethodInfo testMethod,
                                  object[] testMethodArguments,
                                  IReadOnlyList<BeforeAfterTestAttribute> beforeAfterAttributes,
                                  ExceptionAggregator aggregator,
                                  CancellationTokenSource cancellationTokenSource)
            : base(test, messageBus, testClass, constructorArguments, testMethod, testMethodArguments, beforeAfterAttributes, aggregator,
                   cancellationTokenSource)
        {
            _testLifetimeScope = testLifetimeScope;
        }

        protected override object CreateTestClass()
        {
            object testClass = null;

            if (!TestMethod.IsStatic && !Aggregator.HasExceptions)
            {
                if (!MessageBus.QueueMessage(new TestClassConstructionStarting(Test))) CancellationTokenSource.Cancel();

                try { if (!CancellationTokenSource.IsCancellationRequested) Timer.Aggregate(() => testClass = _testLifetimeScope.Resolve(TestClass)); }
                finally { if (!MessageBus.QueueMessage(new TestClassConstructionFinished(Test))) CancellationTokenSource.Cancel(); }
            }

            return testClass;
        }
    }
}
