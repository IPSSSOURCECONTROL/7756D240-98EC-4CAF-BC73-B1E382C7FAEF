using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.WorkflowCommon;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces
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