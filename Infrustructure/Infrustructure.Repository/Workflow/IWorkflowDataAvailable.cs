namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow
{
    public interface IWorkflowDataAvailable<TWorkflowData> where TWorkflowData:class 
    {
        TWorkflowData WorkflowData { get; set; }
    }
}