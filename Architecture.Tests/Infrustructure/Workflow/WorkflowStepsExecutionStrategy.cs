using System.Collections.Generic;

namespace Architecture.Tests.Infrustructure.Workflow
{
    public class WorkflowStepsExecutionStrategy : IWorkflowStepsExecutionStrategy
    {
        public void ProcessSteps<TWorkflowContext>(IList<IWorkflowStep<TWorkflowContext>> steps, TWorkflowContext workflowContext) where TWorkflowContext : class
        {
            foreach (IWorkflowStep<TWorkflowContext> workflowStep in steps)
            {
                workflowStep.WorkflowContext = workflowContext;
                workflowStep.Execute();
            }
        }
    }
}