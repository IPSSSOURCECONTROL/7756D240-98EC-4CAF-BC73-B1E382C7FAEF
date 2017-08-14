namespace Architecture.Tests.Infrustructure.DependencyInjection
{
    /// <summary>
    /// Provides functionality to resolve dependencies. Implement this interface in order to wrap 
    /// a IOC container e.g. Autofac, Castle Windsor, Structure Map etc.
    /// </summary>
    public interface IAutoResolver
    {
        /// <summary>
        /// Resolves a <see cref="TDependency"/> type.
        /// </summary>
        /// <typeparam name="TDependency"></typeparam>
        /// <returns></returns>
        TDependency Resolve<TDependency>() where TDependency : class;
    }
}
