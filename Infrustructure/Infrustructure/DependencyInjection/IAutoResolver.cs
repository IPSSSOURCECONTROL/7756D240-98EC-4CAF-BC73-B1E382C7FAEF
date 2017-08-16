using System;
using Castle.MicroKernel.Registration;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection
{
    /// <summary>
    /// Provides functionality to resolve dependencies. Implement this interface in order to wrap 
    /// a IOC container e.g. Autofac, Castle Windsor, Structure Map etc.
    /// </summary>
    public interface IAutoResolver: IDisposable
    {
        /// <summary>
        /// Resolves a <see cref="TDependency"/> type.
        /// </summary>
        /// <typeparam name="TDependency"></typeparam>
        /// <returns><see cref="TDependency"/> however wll return a null if the dependency is 
        /// not found.</returns>
        TDependency Resolve<TDependency>() where TDependency : class; 
    }

    public interface IWindsorInstallerAvailable
    {
        void RegisterInstaller(IWindsorInstaller installer);
    }
}
