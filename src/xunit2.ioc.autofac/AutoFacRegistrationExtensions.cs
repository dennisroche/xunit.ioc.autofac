using System;
using Autofac.Builder;
using Xunit.Ioc.Autofac;

// ReSharper disable once CheckNamespace
public static class AutoFacXUnitRegistrationExtensions
{
    /// <summary>
    ///     Registers a component so all dependant components will resolve the same shared instance within the test
    ///     lifetime scope.
    /// </summary>
    public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InstancePerTest<TLimit, TActivatorData, TStyle>(
        this IRegistrationBuilder<TLimit, TActivatorData, TStyle> registration)
    {
        if (registration == null) throw new ArgumentNullException(nameof(registration));

        return registration.InstancePerMatchingLifetimeScope(AutofacTestScopes.Test);
    }

    /// <summary>
    ///     Registers a component so all dependant components will resolve the same shared instance within the test class
    ///     lifetime scope.
    /// </summary>
    public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InstancePerTestClass<TLimit, TActivatorData, TStyle>(
        this IRegistrationBuilder<TLimit, TActivatorData, TStyle> registration)
    {
        if (registration == null) throw new ArgumentNullException(nameof(registration));

        return registration.InstancePerMatchingLifetimeScope(AutofacTestScopes.TestClass);
    }

    /// <summary>
    ///     Registers a component so all dependant components will resolve the same shared instance within the test collection
    ///     lifetime scope.
    /// </summary>
    public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InstancePerTestCollection<TLimit, TActivatorData, TStyle>(
        this IRegistrationBuilder<TLimit, TActivatorData, TStyle> registration)
    {
        if (registration == null) throw new ArgumentNullException(nameof(registration));

        return registration.InstancePerMatchingLifetimeScope(AutofacTestScopes.TestCollection);
    }
}
