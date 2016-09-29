using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestFrameworkExecutor : TestFrameworkExecutor<AutofacTestCase>
    {
        public AutofacTestFrameworkExecutor(AssemblyName assemblyName, IContainer container, ISourceInformationProvider sourceInformationProvider, IMessageSink diagnosticMessageSink)
            : base(assemblyName, sourceInformationProvider, diagnosticMessageSink)
        {
            _container = container;
            _testAssembly = new TestAssembly(AssemblyInfo);
        }

        protected override ITestFrameworkDiscoverer CreateDiscoverer()
        {
            return new AutofacTestFrameworkDiscoverer(AssemblyInfo, SourceInformationProvider, DiagnosticMessageSink);
        }

        protected override async void RunTestCases(IEnumerable<AutofacTestCase> testCases, IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions)
        {
            using (var assemblyRunner = new AutofacTestAssemblyRunner(_container, _testAssembly, testCases, DiagnosticMessageSink, executionMessageSink, executionOptions))
            {
                await assemblyRunner.RunAsync();
            }
        }

        private readonly IContainer _container;
        private readonly TestAssembly _testAssembly;
    }
}
