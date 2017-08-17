using System.Collections.Generic;
using System.Reflection;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities
{
    /// <summary>
    /// Provides functionality to inspect various aspects of the stack.
    /// </summary>
    public interface IStackInspector
    {
        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of <see cref="Assembly"/>'s 
        /// in the stack.
        /// </summary>
        /// <returns><see cref="Assembly"/>'s</returns>
        IEnumerable<Assembly> GetAllStackAssemblies();

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of assembly names 
        /// in the stack.
        /// </summary>
        /// <returns><see cref="Assembly"/>'s</returns>
        IEnumerable<string> GetAllStrackAssemblyNames();
    }
}
