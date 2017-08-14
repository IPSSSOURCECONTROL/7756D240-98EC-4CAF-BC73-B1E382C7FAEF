using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Architecture.Tests.Infrustructure.Configuration;
using Architecture.Tests.Infrustructure.Domain;
using Architecture.Tests.Infrustructure.Serialization;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MongoDB.Bson.Serialization;

namespace Architecture.Tests.DependencyInjection.Installers
{
    public class MongoDbMappingInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            List<Type> subTypes = new List<Type>();
            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var boundedContexts =
                container.Resolve<IObjectSerializer>()
                .Deserialize<ApplicationFunctionsConfiguration>(File.ReadAllText(
                    Environment.CurrentDirectory + "//ApplicationFunctionsConfiguration.xml"));

            foreach (Assembly assembly in assemblies)
            {
                foreach (BoundedContext boundedContext in boundedContexts.BoundedContexts)
                {
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
        }

        private void RegisterBsonClassMap(Type type)
        {
            BsonClassMap.LookupClassMap(type);
        }
    }
}
