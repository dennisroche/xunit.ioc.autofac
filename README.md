xUnit Autofac
=============

Use AutoFac to resolve xUnit test cases.

How to use
=============

Install the [Nuget](https://www.nuget.org/packages/xunit-ioc-autofac) package.

    Install-Package xunit-ioc-autofac

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

            Container = builder.Build();
        }
    }
}
```

License
=============

MIT
