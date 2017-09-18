using Castle.Windsor;

namespace Kbit.ControlCentre.MvcDependency
{
    public class ContainerFactory
    {
        private static IWindsorContainer container;
        private static readonly object SyncObject = new object();

        public static void SetContainer(IWindsorContainer windsorContainer)
        {
            container = windsorContainer;
        }


        public static IWindsorContainer Current()
        {
            return container;
        }
    }
}