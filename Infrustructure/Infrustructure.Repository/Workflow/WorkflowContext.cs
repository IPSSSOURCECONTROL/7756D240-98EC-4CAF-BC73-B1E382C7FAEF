using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow
{
    public abstract class WorkflowContext<TEntity>: IWorkflowContext where TEntity : IEntity
    {
        public AuthorizationContext AuthorizationContext { get; set; }
        public TEntity Entity { get; set; }
    }
}