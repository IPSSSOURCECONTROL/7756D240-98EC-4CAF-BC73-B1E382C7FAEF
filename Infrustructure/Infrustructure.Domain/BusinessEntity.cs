namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Domain
{
    public abstract class BusinessEntity : AggregateRoot
    {
        protected BusinessEntity(string businessId)
        {
            this.BusinessId = businessId;
        }

        public string BusinessId { get; private set; } = string.Empty;
    }
}