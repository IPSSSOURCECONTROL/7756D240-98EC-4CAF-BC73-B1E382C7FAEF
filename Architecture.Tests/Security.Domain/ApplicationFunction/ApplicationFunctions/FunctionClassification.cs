namespace Architecture.Tests.Security.Domain.ApplicationFunction.ApplicationFunctions
{
    public abstract class FunctionClassification
    {
        protected FunctionClassification()
        {
            this.DualAuthorizationClassification = new NoDualAuthorizationRequired();
        }
        public DualAuthorizationClassification DualAuthorizationClassification { get; private set; }

        public void RequiresDualAuthorization(bool authorize)
        {
            if(authorize)
                this.DualAuthorizationClassification = new RequiresDualAuthorization();
            else
                this.DualAuthorizationClassification = new NoDualAuthorizationRequired();
        }

        public virtual string GetName()
        {
            return this.GetType().Name;
        }
    }
}