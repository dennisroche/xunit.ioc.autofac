using System;
using Autofac.Builder;
using Xunit.Ioc.Autofac;


// ReSharper disable once CheckNamespace
public static class AutoFacXUnitRegistrationExtensions
{
    /// <summary>
    /// Registers a component so all dependant components will resolve the same shared instance within the test
    /// lifetime scope.
    /// </summary>
    public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InstancePerTest<TLimit, TActivatorData, TStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TStyle> registration)
    {
        if (registration == null)
            throw new ArgumentNullException(nameof(registration));

        return registration.InstancePerMatchingLifetimeScope(AutofacTestInvoker.TestLifetimeScopeTag);
    }
}