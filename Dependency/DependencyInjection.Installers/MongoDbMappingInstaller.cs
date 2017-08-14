using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Configuration;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Serialization;
using MongoDB.Bson.Serialization;

namespace KhanyisaIntel.Kbit.Framework.DependencyInjection.Installers
{
    public class MongoDbMappingInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            List<Type> subTypes = new List<Type>();
            IEnumerable<Assembly> assemblies = this.GetFrameworkAssemblies();

            var boundedContexts =
                container.Resolve<IObjectSerializer>()
                .Deserialize<ApplicationFunctionsConfiguration>(File.ReadAllText(ConfigurationManager.AppSettings["APP_FUNCTIONS_CONFIG_PATH"]));

            foreach (Assembly assembly in assemblies)
            {
                foreach (BoundedContext boundedContext in boundedContexts.BoundedContexts)
                {
                    if(!assembly.FullName.Contains(boundedContext.Name))
                        continue;

                    List<Type> foundTypes = assembly.GetTypes().Where(x => x.BaseType != null &&
                    x.BaseType == typeof(AggregateRoot) &&
                    x.AssemblyQualifiedName.Contains(boundedContext.Name)).ToList();

                    foreach (Type foundType in foundTypes)
                    {
                        if (foundType.IsAbstract)
                        {
                            subTypes.AddRange(assembly.GetTypes().Where(x => x.BaseType != null
                            && x.BaseType == foundType));
                        }
                    }

                    List<Type> otherNonAggregateRootTypes = assembly.GetTypes().Where(x => x.BaseType != null &&
                    x.BaseType != typeof(AggregateRoot) &&
                    x.AssemblyQualifiedName.Contains(boundedContext.Name)).ToList();

                    foreach (Type otherNonAggregateRootType in otherNonAggregateRootTypes)
                    {
                        if (otherNonAggregateRootType.IsAbstract)
                        {
                            subTypes.AddRange(assembly.GetTypes().Where(x => x.BaseType != null
                            && x.BaseType == otherNonAggregateRootType));
                            subTypes.Add(otherNonAggregateRootType);
                        }
                    }

                    subTypes.AddRange(foundTypes);
                }
            }

            subTypes = subTypes.OrderByDescending(x => x.IsAbstract).ToList();

            foreach (Type subType in subTypes)
            {
                this.RegisterBsonClassMap(subType);
            }

            IEnumerable<BsonClassMap> sdasdsd = BsonClassMap.GetRegisteredClassMaps().ToList();
        }

        private IEnumerable<Assembly> GetFrameworkAssemblies()
        {
            List<Assembly> allAssemblies = new List<Assembly>();
            string path = Path.GetDirectoryName(ConfigurationManager.AppSettings["APP_BIN"]);

            var assemblyPaths =
                Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories).Distinct()
                    .Where(x => x.Contains("KhanyisaIntel.Kbit.Framework."));


            foreach (string dll in assemblyPaths)
                allAssemblies.Add(Assembly.LoadFile(dll));

            return allAssemblies;
        }

        private void RegisterBsonClassMap(Type type)
        {
            if (BsonClassMap.IsClassMapRegistered(type))
                return;

            BsonClassMap.LookupClassMap(type);
        }
    }
}
