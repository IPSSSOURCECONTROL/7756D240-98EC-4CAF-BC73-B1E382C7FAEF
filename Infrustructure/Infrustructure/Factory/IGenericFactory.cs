using KhanyisaIntel.Kbit.Framework.Infrustructure.Reflection;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Factory
{
    /// <summary>
    /// Creates instances by using generic parameters.
    /// </summary>
    public interface IGenericFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString">Optional connection string value.</param>
        /// <param name="objectActivator">Optional <see cref="IObjectActivator"/> type.</param>
        /// <returns><see cref="T"/></returns>
        T Create<T>(string connectionString = null, IObjectActivator objectActivator = null) where T : class;
    }
}
