using System.Collections.Generic;
using Autofac;
using Xunit.Abstractions;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestAssemblyRunnerFactory : IAutofacTestAssemblyRunnerFactory
    {
        public AutofacTestAssemblyRunner Create(IContainer container, ITestAssembly testAssembly, IEnumerable<AutofacTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions)
        {
            return new AutofacTestAssemblyRunner(container, testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions);
        }
    }
}
