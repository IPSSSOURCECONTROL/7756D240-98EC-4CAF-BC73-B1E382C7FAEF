using Architecture.Tests.Infrustructure.AOP.Attributes;
using Architecture.Tests.Infrustructure.Workflow;

namespace Architecture.Tests.Infrustructure.Repository
{
    public interface IRepositoryWorkflowAvailable<TWorkflow, TWorkflowContext>: IDatabaseContextAvailable
        where TWorkflow: class
        where TWorkflowContext: class
    {
        TWorkflow Workflow { get; set; }

        [Transactional]
        void InvokeWorkflow(WorkflowOperation workflowOperation, TWorkflowContext workflowContext);
    }
}