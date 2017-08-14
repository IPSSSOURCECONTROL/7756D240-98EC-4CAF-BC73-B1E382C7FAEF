namespace Architecture.Tests.Infrustructure.Workflow
{
    public interface IWorkflowStep<TWorkflowContext> where TWorkflowContext : class
    {
        TWorkflowContext WorkflowContext { get; set; }
        void Execute();
    }
}