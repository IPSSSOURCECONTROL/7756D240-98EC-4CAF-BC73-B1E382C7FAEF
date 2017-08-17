using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using MoreLinq;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities
{
    public class StackInspector : IStackInspector
    {
        private IEnumerable<Assembly> _cachedAssemblies = new List<Assembly>();

        public IEnumerable<Assembly> GetAllStackAssemblies()
        {
            if (this._cachedAssemblies.Any())
                return this._cachedAssemblies;

            List<Assembly> allAssemblies = new List<Assembly>();
            string path = Path.GetDirectoryName(ConfigurationManager.AppSettings["APP_BIN"]);

            IEnumerable<string> assemblyPaths =
                Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories)
                    .Where(x => x.Contains("KhanyisaIntel.Kbit.Framework."));

            foreach (string dll in assemblyPaths)
                allAssemblies.Add(Assembly.LoadFile(dll));

            this._cachedAssemblies = allAssemblies.DistinctBy(x=>x.ManifestModule.Name).OrderBy(x => x.FullName);

            return this._cachedAssemblies;
        }

        public IEnumerable<string> GetAllStrackAssemblyNames()
        {
            string path = Path.GetDirectoryName(ConfigurationManager.AppSettings["APP_BIN"]);

            return 
                Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories).Distinct()
                    .Where(x => x.Contains("KhanyisaIntel.Kbit.Framework."));
        }
    }
}