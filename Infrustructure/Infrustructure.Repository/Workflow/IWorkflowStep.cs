namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow
{
    public interface IWorkflowStep<TWorkflowContext> where TWorkflowContext : class
    {
        TWorkflowContext WorkflowContext { get; set; }
        void Execute();
    }
}