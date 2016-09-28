using System.Reflection;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac
{
    public abstract class AutofacTestFramework : XunitTestFramework
    {
        protected AutofacTestFramework(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink)
        {
        }

        protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
            => new AutofacTestFrameworkExecutor(assemblyName, Container, SourceInformationProvider, DiagnosticMessageSink);

        protected IContainer Container;
    }
}
