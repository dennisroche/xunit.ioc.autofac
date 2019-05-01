using System.Reflection;
using Autofac;
using Xunit.Abstractions;
using Xunit.Ioc.Autofac.TestFramework;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac
{
    public abstract class AutofacTestFramework : Sdk.TestFramework
    {
        protected AutofacTestFramework(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink)
        {
        }

        protected override ITestFrameworkDiscoverer CreateDiscoverer(IAssemblyInfo assemblyInfo) 
            => Container.Resolve<IAutofacTestFrameworkDiscovererFactory>().Create(assemblyInfo, Container, SourceInformationProvider, DiagnosticMessageSink);

        protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
            => Container.Resolve<IAutofacTestFrameworkExecutorFactory>().Create(assemblyName, Container, SourceInformationProvider, DiagnosticMessageSink);

        protected IContainer Container;
    }
}
