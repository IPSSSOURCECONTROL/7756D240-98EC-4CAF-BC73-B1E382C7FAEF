using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository
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