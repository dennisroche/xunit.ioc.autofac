xUnit Autofac  [![Build Master](https://ci.appveyor.com/api/projects/status/mqvl7dyo0auimouw/branch/master?svg=true)](https://ci.appveyor.com/project/dennisroche/xunit-ioc-autofac) [![NuGet Version](http://img.shields.io/nuget/v/xunit2.ioc.autofac.svg?style=flat)](https://www.nuget.org/packages/xunit2.ioc.autofac/) [![Join the chat at https://gitter.im/xunit-ioc-autofac/Lobby](https://badges.gitter.im/xunit-ioc-autofac/Lobby.svg)](https://gitter.im/xunit-ioc-autofac/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
================

Use AutoFac to resolve xUnit test cases.

No longer maintained
====================

**This project is no longer maintained**. We suggest that you migrate your existing solutions to use [Xunit.DependencyInjection](https://github.com/pengweiqhca/Xunit.DependencyInjection) instead. For example:

1. Replace `ConfigureTestFramework` with `Startup`, and make sure that you specify the `ConfigureHost` method and `UseServiceProviderFactory` to specify that you want to use Autofac.
Remove the `TestFramework` attribute.

    ```diff
    diff --git a/ConfigureTestFramework.cs b/ConfigureTestFramework.cs
    index 0c2c4ca..204e781 100644
    --- a/ConfigureTestFramework.cs
    +++ b/ConfigureTestFramework.cs
    @@ -1,27 +1,28 @@
    -[assembly: TestFramework("Your.Test.Project.ConfigureTestFramework", "AssemblyName")]
    -
    namespace Your.Test.Project
    {
    -    public class ConfigureTestFramework : AutofacTestFramework
    +    public class Startup
        {
    -        private const string TestSuffixConvention = "Tests";
    +        public void Configure(ILoggerFactory loggerFactory, XunitTestOutputLoggerProvider loggerProvider)
    +        {
    +            loggerFactory.AddProvider(loggerProvider);
    +        }
    
    -        public ConfigureTestFramework(IMessageSink diagnosticMessageSink)
    -            : base(diagnosticMessageSink)
    +        public void ConfigureHost(IHostBuilder hostBuilder)
            {
    -            var builder = new ContainerBuilder();
    -            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
    -                .Where(t => t.Name.EndsWith(TestSuffixConvention));
    +            hostBuilder
    +                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    +                .ConfigureContainer<ContainerBuilder>(RegisterDependencies);
    +        }
    
    -            builder.Register(context => new TestOutputHelper())
    -                .AsSelf()
    -                .As<ITestOutputHelper>()
    -                .InstancePerLifetimeScope();
    +        public void ConfigureServices(IServiceCollection serviceCollection)
    +        {
    +            serviceCollection.AddTransient<XunitTestOutputLoggerProvider>();
    +        }
    
    +        private void RegisterDependencies(ContainerBuilder builder)
    +        {
                // configure your container
                // e.g. builder.RegisterModule<TestOverrideModule>();
    -
    -            Container = builder.Build();
            }
        }
    }
    ```

2. Remove `UseAutofacTestFramework` from all of your test classes.

    ```diff 
    diff --git a/MyAwesomeTests.cs b/MyAwesomeTests.cs
    index 3a6ffb2..638506d 100644
    --- a/MyAwesomeTests.cs
    +++ b/MyAwesomeTests.cs
    @@ -1,4 +1,3 @@
    -[UseAutofacTestFramework]
    public class MyAwesomeTests
    {
        public MyAwesomeTests()
    ```

How to use
----------

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
                .AsSelf()
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
=======

MIT
