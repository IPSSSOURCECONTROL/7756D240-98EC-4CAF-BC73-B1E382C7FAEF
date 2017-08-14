namespace Architecture.Tests.Infrustructure.Workflow
{
    public interface IWorkflowDataAvailable<TWorkflowData> where TWorkflowData:class 
    {
        TWorkflowData WorkflowData { get; set; }
    }
}