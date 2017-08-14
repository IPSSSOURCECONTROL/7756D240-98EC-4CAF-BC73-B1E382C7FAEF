using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Validation;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository
{
    public abstract class WorkflowRepository<TWorkflow, TWorkflowContext> : IRepositoryWorkflowAvailable<TWorkflow, TWorkflowContext> 
        where TWorkflow: class, IWorkflow<TWorkflowContext>
        where TWorkflowContext: class
    {
        protected WorkflowRepository(IDatabaseContext databaseContext)
        {
            Validator.CheckReferenceTypeForNull(databaseContext, nameof(databaseContext), MethodBase.GetCurrentMethod());
            this.DatabaseContext = databaseContext;
        }

        public IDatabaseContext DatabaseContext { get; set; }
        public TWorkflow Workflow { get; set; }

        [Transactional]
        public void InvokeWorkflow(WorkflowOperation workflowOperation, TWorkflowContext workflowContext)
        {
            this.Workflow.WorkflowContext = workflowContext;
            this.Workflow.Execute(workflowOperation, workflowContext);
        }
    }
}