using System.Reflection;
using Autofac;
using Xunit.Abstractions;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestFrameworkExecutorFactory : IAutofacTestFrameworkExecutorFactory
    {
        public AutofacTestFrameworkExecutor Create(AssemblyName assemblyName, IContainer container, ISourceInformationProvider sourceInformationProvider, IMessageSink diagnosticMessageSink)
        {
            return new AutofacTestFrameworkExecutor(assemblyName, container, sourceInformationProvider, diagnosticMessageSink);
        }
    }
}
