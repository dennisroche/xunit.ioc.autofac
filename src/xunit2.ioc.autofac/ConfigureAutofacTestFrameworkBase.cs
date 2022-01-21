using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit.Abstractions;
using Xunit.Ioc.Autofac.TestFramework;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac
{
    public abstract class ConfigureAutofacTestFrameworkBase : AutofacTestFramework
    {
        public ConfigureAutofacTestFrameworkBase(Assembly subClassAssembly, IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(subClassAssembly)
                .Where(t => t.GetTypeInfo().IsDefined(typeof(UseAutofacTestFrameworkAttribute)));

            builder.Register(context => new TestOutputHelper())
                .AsSelf()
                .As<ITestOutputHelper>()
                .InstancePerLifetimeScope();

            RegisterFramework(builder);
            SetupAutofac(builder);

            Container = builder.Build();
        }

        protected virtual void RegisterFramework(ContainerBuilder builder)
        {
            builder.RegisterType<AutofacTestAssemblyRunnerFactory>().As<IAutofacTestAssemblyRunnerFactory>();
            builder.RegisterType<AutofacTestCaseFactory>().As<IAutofacTestCaseFactory>();
            builder.RegisterType<AutofacTestCaseRunnerFactory>().As<IAutofacTestCaseRunnerFactory>();
            builder.RegisterType<AutofacTestClassRunnerFactory>().As<IAutofacTestClassRunnerFactory>();
            builder.RegisterType<AutofacTestCollectionRunnerFactory>().As<IAutofacTestCollectionRunnerFactory>();
            builder.RegisterType<AutofacTestFactory>().As<IAutofacTestFactory>();
            builder.RegisterType<AutofacTestFrameworkDiscovererFactory>().As<IAutofacTestFrameworkDiscovererFactory>();
            builder.RegisterType<AutofacTestFrameworkExecutorFactory>().As<IAutofacTestFrameworkExecutorFactory>();
            builder.RegisterType<AutofacTestMethodRunnerFactory>().As<IAutofacTestMethodRunnerFactory>();
            builder.RegisterType<AutofacTestRunnerFactory>().As<IAutofacTestRunnerFactory>();
            builder.RegisterType<AutofacTestInvokerFactory>().As<IAutofacTestInvokerFactory>();
        }

        protected abstract void SetupAutofac(ContainerBuilder builder);

    }
}
