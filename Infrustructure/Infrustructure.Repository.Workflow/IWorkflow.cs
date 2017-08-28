using KhanyisaIntel.Kbit.Framework.Infrustructure.WorkflowCommon;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow
{
    /// <summary>
    /// Defines functionality to execute a <see cref="IWorkflow{TWorkflowContext}"/> workflow.
    /// </summary>
    /// <typeparam name="TWorkflowContext"></typeparam>
    public interface IWorkflow<TWorkflowContext>
        where TWorkflowContext:class
    {
        /// <summary>
        /// The workflow context class containing properties needed by the workflow.
        /// </summary>
        TWorkflowContext WorkflowContext { get; set; }

        /// <summary>
        /// Executes all registered workflow steps.
        /// </summary>
        /// <param name="workflowOperation">Workflow operation.</param>
        /// <param name="workflowContext">The workflow context type.</param>
        void Execute(WorkflowOperation workflowOperation, TWorkflowContext workflowContext);
    }
}
