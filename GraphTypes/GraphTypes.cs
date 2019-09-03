using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.With.Rest.Api
{
    public class AddressGraphType : ObjectGraphType<Address>
    {
        public AddressGraphType()
        {
            Name = "Address";
            Field(x => x.AddressId, type: typeof(IdGraphType)).Description("AddressId");
            Field(x => x.AddressLine1, type: typeof(IdGraphType)).Description("AddressLine1");
            Field(x => x.AddressLine2, type: typeof(IdGraphType)).Description("AddressLine2");
        }
    }

    public class UserDetailGraphType : ObjectGraphType<UserDetail>
    {
        public UserDetailGraphType()
        {
            Name = "UserDetail";
            Field(
                    name: "Address",
                    type: typeof(AddressGraphType),
                    resolve: context => context.Source.Address
                );
            Field(
                    name: "CreditCardInfo",
                    type:typeof(CreditCardInfoGraphType),
                    resolve:context => context.Source.CreditCardInfo
                );
            Field(x => x.UserId, type: typeof(IdGraphType)).Description("UserId");
            Field(x => x.FirstName, type: typeof(StringGraphType)).Description("FirstName");
            Field(x => x.LastName, type: typeof(StringGraphType)).Description("LastName");
            Field(x => x.Email, type: typeof(StringGraphType)).Description("Email");
            Field(x => x.IsPlatinumUser, type: typeof(BooleanGraphType)).Description("IsPlatinumUser");
            Field(x => x.CreatedDate, type: typeof(DateTimeGraphType)).Description("CreatedDate");
        }
    }

    public class CreditCardInfoGraphType : ObjectGraphType<CreditCardInfo>
    {
        public CreditCardInfoGraphType()
        {
            Name = "CreditCardInfo";
            Field(x => x.CreditCardNumber, type: typeof(StringGraphType)).Description("CreditCardNumber");
            Field(x => x.ExpiryDate, type: typeof(DateTimeGraphType)).Description("ExpiryDate");
            Field(x => x.CVVNumber, type: typeof(StringGraphType)).Description("CVVNumber");
            Field(x => x.DisplayName, type: typeof(StringGraphType)).Description("DisplayName");

        }
    }

    public class CreditCardTransactionGraphType : ObjectGraphType<CreditCardTransaction>
    {
        public CreditCardTransactionGraphType()
        {
            Name = "CreditCardTransaction";
            Field(x => x.CreditCardNumber, type: typeof(StringGraphType)).Description("CreditCardNumber");
            Field(x => x.TransactionDate, type: typeof(DateTimeGraphType)).Description("TransactionDate");
            Field(x => x.AmountSpent, type: typeof(StringGraphType)).Description("AmountSpent");
            Field(x => x.PurchasedFromCompanyName, type: typeof(StringGraphType)).Description("PurchasedFromCompanyName");
        }
    }
}
