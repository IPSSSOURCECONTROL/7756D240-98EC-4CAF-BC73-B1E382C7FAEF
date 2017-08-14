using System.Reflection;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using Architecture.Tests.Infrustructure.MongoDb;
using Architecture.Tests.Infrustructure.Validation;
using Architecture.Tests.Infrustructure.Workflow;

namespace Architecture.Tests.Infrustructure.Repository
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