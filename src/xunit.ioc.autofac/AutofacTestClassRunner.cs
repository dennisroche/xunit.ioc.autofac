using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac
{
    public class AutofacTestClassRunner : XunitTestClassRunner
    {
        public AutofacTestClassRunner(IContainer container, ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<IXunitTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageBus messageBus,
            ITestCaseOrderer testCaseOrderer, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource, IDictionary<Type, object> collectionFixtureMappings)
            : base(testClass, @class, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource, collectionFixtureMappings)
        {
            _scope = container.BeginLifetimeScope();
        }

        protected override bool TryGetConstructorArgument(ConstructorInfo constructor, int index, ParameterInfo parameter, out object argumentValue)
        {
            if (parameter.ParameterType != typeof(ITestOutputHelper))
                return _scope.TryResolve(parameter.ParameterType, out argumentValue);

            argumentValue = new TestOutputHelper();
            return true;
        }

        protected override Task BeforeTestClassFinishedAsync()
        {
            _scope.Dispose();
            return base.BeforeTestClassFinishedAsync();
        }

        private readonly ILifetimeScope _scope;
    }
}