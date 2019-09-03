using System;

namespace GraphQL.With.Rest.Api
{
    public class UserDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public bool IsPlatinumUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public Address Address { get; set; }
        public CreditCardInfo CreditCardInfo { get; set; }
    }

    public class Address
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
    }

    public class CreditCardInfo
    {
        public string DisplayName { get; set; }
        public string CreditCardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVVNumber { get; set; }
    }

    public class CreditCardTransaction
    {
        public string CreditCardNumber { get; set; }
        public string AmountSpent { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PurchasedFromCompanyName { get; set; }
    }
}
