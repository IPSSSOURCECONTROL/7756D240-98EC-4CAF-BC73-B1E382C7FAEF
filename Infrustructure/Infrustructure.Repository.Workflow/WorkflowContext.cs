using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Logging;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Security;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow
{
    public abstract class WorkflowContext<TEntity>: IWorkflowContext where TEntity : IEntity
    {
        public AuthorizationContext AuthorizationContext { get; set; }

        public TEntity Entity { get; set; }

        [MandatoryInjection]
        public ILoggingType Logger { get; set; }
    }
}