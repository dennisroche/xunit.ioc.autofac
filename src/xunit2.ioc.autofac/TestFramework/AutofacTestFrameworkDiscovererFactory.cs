using Autofac;
using Xunit.Abstractions;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestFrameworkDiscovererFactory : IAutofacTestFrameworkDiscovererFactory
    {
        public AutofacTestFrameworkDiscoverer Create(IAssemblyInfo assemblyInfo, IContainer container, ISourceInformationProvider sourceProvider, IMessageSink diagnosticMessageSink)
        {
            return new AutofacTestFrameworkDiscoverer(assemblyInfo, container, sourceProvider, diagnosticMessageSink);
        }
    }


}
