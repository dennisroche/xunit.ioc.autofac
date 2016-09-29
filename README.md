xUnit Autofac  [![Build Master](https://ci.appveyor.com/api/projects/status/mqvl7dyo0auimouw/branch/master?svg=true)](https://ci.appveyor.com/project/dennisroche/xunit-ioc-autofac) [![NuGet Version](http://img.shields.io/nuget/v/xunit2.ioc.autofac.svg?style=flat)](https://www.nuget.org/packages/xunit2.ioc.autofac/)
================

Use AutoFac to resolve xUnit test cases.

How to use
=============

Install the [Nuget](https://www.nuget.org/packages/xunit2.ioc.autofac) package.

    Install-Package xunit2.ioc.autofac

In your testing project, add the following framework

```cs
[assembly: TestFramework("Your.Test.Project.ConfigureTestFramework", "AssemblyName")]

namespace Your.Test.Project
{
    public class ConfigureTestFramework : AutofacTestFramework
    {
        public ConfigureTestFramework(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink)
        {
            var builder = new ContainerBuilder();
            // configure your container
            // e.g. builder.RegisterModule<TestOverrideModule>();

            builder.Register(context => new TestOutputHelper())
                .As<ITestOutputHelper>()
                .InstancePerDependency();

            Container = builder.Build();
        }
    }
}
```

License
=============

MIT
