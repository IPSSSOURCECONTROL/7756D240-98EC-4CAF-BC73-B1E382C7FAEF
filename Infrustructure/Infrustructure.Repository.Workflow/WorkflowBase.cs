using System.Collections.Generic;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Validation;
using KhanyisaIntel.Kbit.Framework.Infrustructure.WorkflowCommon;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow
{
    public abstract class WorkflowBase<TWorkflowContext, TWorkflowData> : IWorkflow<TWorkflowContext>, IWorkflowDataAvailable<TWorkflowData> where TWorkflowContext: class, IWorkflowContext
        where TWorkflowData : class
    {
        protected IWorkflowStepsExecutionStrategy WorkflowStepsExecutionStrategy;
        private TWorkflowData _workflowData;

        public TWorkflowData WorkflowData
        {
            get
            {
                ValidationUtility.CheckReferenceTypeForNull(this._workflowData,
                    $"WorkflowData of type '{typeof(TWorkflowData).Name}' " +
                    $"not set. Did you forget to initialize the property? " +
                    $"Set it in the construtor of the concreate '{typeof(TWorkflowData)}'.", MethodBase.GetCurrentMethod(), this.GetType());

                return this._workflowData;
            }
            set { this._workflowData = value; }
        }

        public TWorkflowContext WorkflowContext { get; set; }
        protected IObjectActivator ObjectActivator { get; set; }
        private IList<IWorkflowStep<TWorkflowContext>> AddWorkflowSteps { get; } = new List<IWorkflowStep<TWorkflowContext>>();
        private IList<IWorkflowStep<TWorkflowContext>> ModifyWorkflowSteps { get; } = new List<IWorkflowStep<TWorkflowContext>>();
        private IList<IWorkflowStep<TWorkflowContext>> RemoveWorkflowSteps { get; } = new List<IWorkflowStep<TWorkflowContext>>();

        protected virtual void RegisterWorkflowStepsExecutionStrategy()
        {
            this.WorkflowStepsExecutionStrategy = new WorkflowStepsExecutionStrategy();
        }

        protected virtual void RegisterSteps()
        {

        }


        public void AddStep<TWorkflowStep>(WorkflowOperation workflowOperation) where TWorkflowStep: class 
        {
            IWorkflowStep<TWorkflowContext> type = (IWorkflowStep<TWorkflowContext>)this.ObjectActivator.CreateInstanceOf(
                typeof(TWorkflowStep), this.WorkflowData, this.WorkflowContext);
            if (type == null)
                return;

            switch (workflowOperation)
            {
                case WorkflowOperation.Add:
                    this.AddWorkflowSteps.Add(type);
                    break;
                case WorkflowOperation.Update:
                    this.ModifyWorkflowSteps.Add(type);
                    break;
                case WorkflowOperation.Remove:
                    this.RemoveWorkflowSteps.Add(type);
                    break;
            }
        }

        public void Execute(WorkflowOperation workflowOperation, TWorkflowContext workflowContext)
        {
            this.WorkflowContext = workflowContext;

            this.RegisterSteps();
            this.RegisterWorkflowStepsExecutionStrategy();

            switch (workflowOperation)
            {
                case WorkflowOperation.Add:
                    this.WorkflowStepsExecutionStrategy.ProcessSteps(this.AddWorkflowSteps, workflowContext);
                    break;
                case WorkflowOperation.Update:
                    this.WorkflowStepsExecutionStrategy.ProcessSteps(this.ModifyWorkflowSteps, workflowContext);
                    break;
                case WorkflowOperation.Remove:
                    this.WorkflowStepsExecutionStrategy.ProcessSteps(this.RemoveWorkflowSteps, workflowContext);
                    break;
            }
        }
    }
}