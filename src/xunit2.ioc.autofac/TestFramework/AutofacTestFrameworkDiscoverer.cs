using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestFrameworkDiscoverer : XunitTestFrameworkDiscoverer
    {
        private readonly AutofacFactDiscoverer _autofacFactDiscoverer;
        private readonly AutofacTheoryDiscoverer _autofacTheoryDiscoverer;

        public AutofacTestFrameworkDiscoverer(IAssemblyInfo assemblyInfo, ISourceInformationProvider sourceProvider, IMessageSink diagnosticMessageSink)
            : base(assemblyInfo, sourceProvider, diagnosticMessageSink)
        {
            _autofacFactDiscoverer = new AutofacFactDiscoverer(diagnosticMessageSink);
            _autofacTheoryDiscoverer = new AutofacTheoryDiscoverer(diagnosticMessageSink);
        }

        protected override bool FindTestsForType(ITestClass testClass,
                                                 bool includeSourceInformation,
                                                 IMessageBus messageBus,
                                                 ITestFrameworkDiscoveryOptions discoveryOptions)
        {
            var hasAttribute = testClass.Class.GetCustomAttributes(typeof(UseAutofacTestFrameworkAttribute)).Any();
            if (!hasAttribute) return base.FindTestsForType(testClass, includeSourceInformation, messageBus, discoveryOptions);

            foreach (var method in testClass.Class.GetMethods(true))
            {
                var testMethod = new TestMethod(testClass, method);
                if (!FindTestsForMethodOnAutofacTestClass(testMethod, includeSourceInformation, messageBus, discoveryOptions)) return false;
            }

            return true;
        }

        private bool FindTestsForMethodOnAutofacTestClass(ITestMethod testMethod,
                                                          bool includeSourceInformation,
                                                          IMessageBus messageBus,
                                                          ITestFrameworkDiscoveryOptions discoveryOptions)
        {
            var factAttributes = testMethod.Method.GetCustomAttributes(typeof(FactAttribute)).ToList();
            if (factAttributes.Count > 1)
            {
                var message = $"Test method '{testMethod.TestClass.Class.Name}.{testMethod.Method.Name}' has multiple [Fact]-derived attributes";
                var testCase = new ExecutionErrorTestCase(DiagnosticMessageSink, TestMethodDisplay.ClassAndMethod, testMethod, message);
                return ReportDiscoveredTestCase(testCase, includeSourceInformation, messageBus);
            }

            var factAttribute = factAttributes.FirstOrDefault();

            if (factAttribute == null) return true;

            var factAttributeType = (factAttribute as IReflectionAttributeInfo)?.Attribute.GetType();

            IXunitTestCaseDiscoverer discoverer = null;
            if (factAttributeType == typeof(FactAttribute)) discoverer = _autofacFactDiscoverer;
            else if (factAttributeType == typeof(TheoryAttribute)) discoverer = _autofacTheoryDiscoverer;

            if (discoverer == null) return true;

            foreach (var testCase in discoverer.Discover(discoveryOptions, testMethod, factAttribute))
                if (!ReportDiscoveredTestCase(testCase, includeSourceInformation, messageBus)) return false;

            return true;
        }
    }
}
