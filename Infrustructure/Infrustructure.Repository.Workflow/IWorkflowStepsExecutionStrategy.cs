using System.Collections.Generic;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow
{
    public interface IWorkflowStepsExecutionStrategy
    {
        void ProcessSteps<TWorkflowContext>(IList<IWorkflowStep<TWorkflowContext>> steps, 
            TWorkflowContext workflowContext) where TWorkflowContext:class;
    }
}