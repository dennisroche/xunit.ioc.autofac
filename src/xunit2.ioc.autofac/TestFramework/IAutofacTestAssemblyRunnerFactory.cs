using System.Collections.Generic;
using Autofac;
using Xunit.Abstractions;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public interface IAutofacTestAssemblyRunnerFactory
    {
        AutofacTestAssemblyRunner Create(IContainer container, ITestAssembly testAssembly, IEnumerable<AutofacTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageSink executionMessageSink,
            ITestFrameworkExecutionOptions executionOptions);
    }
}
