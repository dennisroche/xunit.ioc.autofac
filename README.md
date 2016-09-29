xUnit Autofac  [![Build Master](https://ci.appveyor.com/api/projects/status/mqvl7dyo0auimouw/branch/master?svg=true)](https://ci.appveyor.com/project/dennisroche/xunit-ioc-autofac) [![NuGet Version](http://img.shields.io/nuget/v/xunit2.ioc.autofac.svg?style=flat)](https://www.nuget.org/packages/xunit2.ioc.autofac/)
================

[![Join the chat at https://gitter.im/xunit-ioc-autofac/Lobby](https://badges.gitter.im/xunit-ioc-autofac/Lobby.svg)](https://gitter.im/xunit-ioc-autofac/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

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
        private const string TestSuffixConvention = "Tests";

        public ConfigureTestFramework(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith(TestSuffixConvention));

            builder.Register(context => new TestOutputHelper())
                .As<ITestOutputHelper>()
                .InstancePerLifetimeScope();

            // configure your container
            // e.g. builder.RegisterModule<TestOverrideModule>();

            Container = builder.Build();
        }
    }
}
```

Example test `class`.

```cs
[UseAutofacTestFramework]
public class MyAwesomeTests
{
    public MyAwesomeTests()
    {
    }

    public MyAwesomeTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void AssertThatWeDoStuff()
    {
        _outputHelper.WriteLine("Hello");
    }

    private readonly ITestOutputHelper _outputHelper;
}
```

License
=============

MIT
