using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    internal class AutofacTheoryDiscoverer : TheoryDiscoverer
    {
        public AutofacTheoryDiscoverer(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink) { }

        protected override IEnumerable<IXunitTestCase> CreateTestCasesForTheory(ITestFrameworkDiscoveryOptions discoveryOptions,
                                                                                ITestMethod testMethod,
                                                                                IAttributeInfo theoryAttribute) =>
            new[] {new AutofacTheoryTestCase(DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod)};

        protected override IEnumerable<IXunitTestCase> CreateTestCasesForDataRow(ITestFrameworkDiscoveryOptions discoveryOptions,
                                                                                 ITestMethod testMethod,
                                                                                 IAttributeInfo theoryAttribute,
                                                                                 object[] dataRow)
        {
            return new[] {new AutofacTheoryTestCase(DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod, dataRow)};
        }

        protected override IEnumerable<IXunitTestCase> CreateTestCasesForSkip(ITestFrameworkDiscoveryOptions discoveryOptions,
                                                                              ITestMethod testMethod,
                                                                              IAttributeInfo theoryAttribute,
                                                                              string skipReason)
        {
            return new[] {new AutofacTheoryTestCase(DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod)};
        }

        protected override IEnumerable<IXunitTestCase> CreateTestCasesForSkippedDataRow(ITestFrameworkDiscoveryOptions discoveryOptions,
                                                                                        ITestMethod testMethod,
                                                                                        IAttributeInfo theoryAttribute,
                                                                                        object[] dataRow,
                                                                                        string skipReason)
        {
            return new[] {new AutofacSkippedDataRowTestCase(DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod, skipReason, dataRow)};
        }
    }
}
 
