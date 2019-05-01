using System.Reflection;
using Autofac;
using Xunit.Abstractions;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public interface IAutofacTestFrameworkExecutorFactory
    {
        AutofacTestFrameworkExecutor Create(AssemblyName assemblyName, IContainer container, ISourceInformationProvider sourceInformationProvider, IMessageSink diagnosticMessageSink);
    }
}
