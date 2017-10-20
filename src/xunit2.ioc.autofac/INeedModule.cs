using Autofac;

namespace Xunit.Ioc.Autofac
{
    // ReSharper disable once UnusedTypeParameter
    public interface INeedModule<T> where T : Module, new() { }
}
