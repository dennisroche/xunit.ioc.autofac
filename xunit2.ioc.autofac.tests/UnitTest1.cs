using System;
using Xunit;
using Xunit.Ioc.Autofac;

namespace xunit2.ioc.autofac.tests
{
    [UseAutofacTestFrameworkAttribute]
    public class UnitTest1
    {
        private readonly TestService service;


        public UnitTest1(TestService service)
        {
            this.service = service;
        }

        [Fact]
        public void Test1()
        {
            Assert.True(service.GetValue() == "abcd");
        }
    }
}
