using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac
{
    public class AutofacTestFrameworkExecutor : XunitTestFrameworkExecutor
    {
        public AutofacTestFrameworkExecutor(AssemblyName assemblyName, IContainer container, ISourceInformationProvider sourceInformationProvider, IMessageSink diagnosticMessageSink)
            : base(assemblyName, sourceInformationProvider, diagnosticMessageSink)
        {
            _container = container;
        }

        protected override async void RunTestCases(IEnumerable<IXunitTestCase> testCases, IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions)
        {
            using (var assemblyRunner = new AutofacTestAssemblyRunner(_container, TestAssembly, testCases, DiagnosticMessageSink, executionMessageSink, executionOptions))
            {
                await assemblyRunner.RunAsync();
            }
        }

        private readonly IContainer _container;
    }
}