using Richmond.Domain.DomainObjects;
using System;

namespace Richmond.Domain.Entities
{
    public class Account : Entity
    {
        public AccountStatus AccountStatus { get; private set; }
        public string AddressBody { get; private set; }
        public string Aref { get; private set; }
        public string AuthorisedBank { get; private set; }
        public string AuthorisedCard { get; private set; }
        public Guid CampaignId { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Email Email { get; private set; }
        public string FirstName { get; private set; }
        public Guid GovernmentID { get; private set; }
        public Guid LenderId { get; private set; }
        public string MiddleName { get; private set; }
        public decimal MonthlyFee { get; private set; }
        public Guid OriginatorId { get; private set; }
        public string Postcode { get; private set; }
        public DateTime RegularPaymentDay { get; private set; }
        public string State { get; private set; }
        public string Surname { get; private set; }
        public string Tel { get; private set; }
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public decimal ExpectedPaymentAmount { get; private set; }
        public string Contract { get; private set; }
        public DateTime FirstPaymentDateUtc { get; private set; }
        private Account() { }
        protected Account(string addressBody, string aref,
                       string authorisedBank, string authorisedCard, Guid campaignId,
                       DateTime dateOfBirth, string email, string firstName, Guid governmentID,
                       Guid lenderId, string middleName, decimal monthlyFee, Guid originatorId,
                       string postcode, DateTime? regularPaymentDay, string state, string surname,
                       string tel, Product product, decimal expectedPaymentAmount, string contract)
        {
            AccountStatus = AccountStatus.Live;
            AuthorisedBank = string.Empty;
            AuthorisedCard = string.Empty;
            AddressBody = addressBody;
            Aref = aref;
            CampaignId = campaignId;
            DateOfBirth = dateOfBirth;
            Email = new Email(email);
            FirstName = firstName;
            GovernmentID = governmentID;
            LenderId = lenderId;
            MiddleName = middleName;
            MonthlyFee = monthlyFee;
            OriginatorId = originatorId;
            Postcode = postcode;
            RegularPaymentDay = regularPaymentDay ?? DateTime.UtcNow;
            State = state;
            Surname = surname;
            Tel = tel;
            Product = product;
            ExpectedPaymentAmount = expectedPaymentAmount;
            Contract = contract;
            DateCreated = DateTime.UtcNow;
            if (Product != null && Product.SetupFee <= 0)
            {
                FirstPaymentDateUtc.AddDays(30);
            }
        }
        public void SetBankingAndCard(string authorisedBank,string authorisedCard)
        {
            AuthorisedBank = authorisedBank;
            AuthorisedCard = authorisedCard;
        }
        public static class AccountFactory
        {
            public static Account NewAccount(string addressBody, string aref,
                       string authorisedBank, string authorisedCard, Guid campaignId,
                       DateTime dateOfBirth, string email, string firstName, Guid governmentID,
                       Guid lenderId, string middleName, decimal monthlyFee, Guid originatorId,
                       string postcode, DateTime? regularPaymentDay, string state, string surname,
                       string tel, Product product, decimal expectedPaymentAmount, string contract)
            {
                return new Account(addressBody, aref,
                        authorisedBank, authorisedCard, campaignId,
                       dateOfBirth, email, firstName, governmentID,
                       lenderId, middleName, monthlyFee, originatorId,
                       postcode, regularPaymentDay, state, surname,
                       tel, product, expectedPaymentAmount, contract);
            }
        }

        public override string ToString()
        {
            return $"{FirstName}.{MiddleName}.{Surname} [Status={AccountStatus}]";
        }
    }
    public enum AccountStatus
    {
        Live = 1
    }
}
