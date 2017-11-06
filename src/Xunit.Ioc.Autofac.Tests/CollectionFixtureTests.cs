using Autofac;
using FluentAssertions;

namespace Xunit.Ioc.Autofac.Tests
{
    [UseAutofacTestFramework]
    [Collection("Foo")]
    public class CollectionFixtureTests
    {
        public CollectionFixtureTests(ILifetimeScope lifetimeScope, IFoo foo)
        {
            _lifetimeScope = lifetimeScope;
            _foo = foo;
        }

        private readonly ILifetimeScope _lifetimeScope;
        private readonly IFoo _foo;

        [Fact]
        public void Collections_work_correcetly()
        {
            _foo.Should().NotBeNull();
            _lifetimeScope.Resolve<FooClassFixture>().Should().NotBeNull();
            _lifetimeScope.Resolve<FooCollectionFixture>().Should().NotBeNull();
        }

        [Theory]
        [InlineData("ABC-02-04-CDE")]
        [InlineData("ABC-02-40-CDE")]
        [InlineData("ABC-02-0040-CDE")]
        public void Theories_work_correctly(string input)
        {
            input.Should().Be(input);
        }
    }

    [CollectionDefinition("Foo")]
    public class FooCollectionDefinition : ICollectionFixture<FooCollectionFixture>, IClassFixture<FooClassFixture> { }

    public class FooCollectionFixture : INeedModule<FooModule> { }

    public class FooClassFixture { }

    public class FooModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Foo>().As<IFoo>();
        }
    }

    public interface IFoo { }

    internal class Foo : IFoo { }
}
