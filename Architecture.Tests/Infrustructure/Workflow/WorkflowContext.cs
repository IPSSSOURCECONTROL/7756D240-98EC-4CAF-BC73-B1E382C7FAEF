using Architecture.Tests.Infrustructure.Domain;

namespace Architecture.Tests.Infrustructure.Workflow
{
    public abstract class WorkflowContext<TEntity>: IWorkflowContext where TEntity : IEntity
    {
        public AuthorizationContext AuthorizationContext { get; set; }
        public TEntity Entity { get; set; }
    }
}