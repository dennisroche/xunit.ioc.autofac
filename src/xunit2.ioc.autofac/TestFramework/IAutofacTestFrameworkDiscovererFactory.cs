using Autofac;
using Xunit.Abstractions;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public interface IAutofacTestFrameworkDiscovererFactory
    {
        AutofacTestFrameworkDiscoverer Create(IAssemblyInfo assemblyInfo, IContainer container, ISourceInformationProvider sourceProvider, IMessageSink diagnosticMessageSink);
    }


}
