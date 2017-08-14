using System.Collections.Generic;

namespace Architecture.Tests.Infrustructure.Workflow
{
    public interface IWorkflowStepsExecutionStrategy
    {
        void ProcessSteps<TWorkflowContext>(IList<IWorkflowStep<TWorkflowContext>> steps, TWorkflowContext workflowContext) where TWorkflowContext:class;
    }
}