using Autofac;
using Xunit;
using Xunit.Abstractions;

[assembly:TestFramework("Xunit.Ioc.Autofac.Tests.TestFramework", "Xunit.Ioc.Autofac.Tests")]

namespace Xunit.Ioc.Autofac.Tests
{
    public class TestFramework : AutofacTestFramework
    {
        public TestFramework(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink) { }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            
        }
    }
}
