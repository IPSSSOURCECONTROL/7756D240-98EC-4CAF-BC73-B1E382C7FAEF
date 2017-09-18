namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Domain
{
    public abstract class BusinessEntity : AggregateRoot, IBusinessLink
    {
        protected BusinessEntity()
        {
        }

        protected BusinessEntity(string businessId)
        {
            this.BusinessId = businessId;
        }

        public virtual string BusinessId { get; private set; }
    }
}