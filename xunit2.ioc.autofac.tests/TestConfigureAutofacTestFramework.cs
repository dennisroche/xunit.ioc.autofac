using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Xunit;
using Xunit.Abstractions;
using Xunit.Ioc.Autofac;


[assembly: TestFramework("xunit2.ioc.autofac.tests.TestConfigureAutofacTestFramework", "xunit2.ioc.autofac.tests")]

namespace xunit2.ioc.autofac.tests
{
    public class TestConfigureAutofacTestFramework : ConfigureAutofacTestFrameworkBase
    {
        public TestConfigureAutofacTestFramework(IMessageSink diagnosticMessageSink) : base(Assembly.GetExecutingAssembly(), diagnosticMessageSink)
        {
        }

        protected override void SetupAutofac(ContainerBuilder builder)
        {
            builder.RegisterType<TestService>();
        }
    }
}
