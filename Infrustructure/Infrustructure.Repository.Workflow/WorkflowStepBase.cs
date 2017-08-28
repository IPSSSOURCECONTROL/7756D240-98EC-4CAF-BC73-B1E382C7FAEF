using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow
{
    public abstract class WorkflowStepBase<TWorkflowContext, TRepository>: 
        IWorkflowStep<TWorkflowContext> 
        where TWorkflowContext : class 
        where TRepository: IDatabaseContextAvailable
    {
        protected WorkflowStepBase(TRepository repository, TWorkflowContext context)
        {
            this.WorkflowContext = context;
            this.Repository = repository;
        }

        public TWorkflowContext WorkflowContext { get; set; }

        public TRepository Repository { get; }

        protected virtual void OnPretransaction()
        {

        }

        protected virtual void OnTransaction()
        {
        }

        public void Execute()
        {
            this.OnPretransaction();
            this.OnTransaction();
            this.Repository.DatabaseContext.SaveChanges();
            this.OnPosttransaction();
        }

        protected virtual void OnPosttransaction()
        {

        }
    }
}