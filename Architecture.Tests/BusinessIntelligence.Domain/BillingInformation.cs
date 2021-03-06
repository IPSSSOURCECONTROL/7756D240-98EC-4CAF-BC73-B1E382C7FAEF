using Architecture.Tests.Infrustructure.Domain;

namespace Architecture.Tests.BusinessIntelligence.Domain
{
    public class BillingInformation: IValueObject
    {
        public BillingInformation(string bank, string accountNumber, string branchCode, string reference)
        {
            this.Bank = bank;
            this.AccountNumber = accountNumber;
            this.BranchCode = branchCode;
            this.Reference = reference;
        }

        public string Bank { get; }
        public string AccountNumber { get; }
        public string BranchCode { get; }
        public string Reference { get; }
    }
}