using Autofac;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestFrameworkDiscoverer : TestFrameworkDiscoverer
    {
        public AutofacTestFrameworkDiscoverer(IAssemblyInfo assemblyInfo, IContainer container, ISourceInformationProvider sourceProvider, IMessageSink diagnosticMessageSink)
            : base(assemblyInfo, sourceProvider, diagnosticMessageSink)
        {
            var testAssembly = new TestAssembly(assemblyInfo);
            _testCollectionFactory = new CollectionPerClassTestCollectionFactory(testAssembly, diagnosticMessageSink);
            this._container = container;
        }

        protected override ITestClass CreateTestClass(ITypeInfo @class)
        {
            return new TestClass(_testCollectionFactory.Get(@class), @class);
        }

        protected override bool FindTestsForType(ITestClass testClass, bool includeSourceInformation, IMessageBus messageBus, ITestFrameworkDiscoveryOptions discoveryOptions)
        {
            var methodDisplay = discoveryOptions.MethodDisplayOrDefault();

            var hasAttribute = testClass.Class.GetCustomAttributes(typeof(UseAutofacTestFrameworkAttribute)).Any();
            if (!hasAttribute)
                return true;

            foreach (var method in testClass.Class.GetMethods(includePrivateMethods: true))
            {
                if (!method.GetCustomAttributes(typeof(FactAttribute)).Any())
                    continue;

                var testMethod = new TestMethod(testClass, method);
                var testCase = _container.Resolve<IAutofacTestCaseFactory>().Create(methodDisplay, testMethod);

                if (!ReportDiscoveredTestCase(testCase, includeSourceInformation, messageBus))
                    return false;
            }

            return true;
        }

        private readonly CollectionPerClassTestCollectionFactory _testCollectionFactory;
        private readonly IContainer _container;
    }

}
