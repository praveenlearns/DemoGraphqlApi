using Faker;
using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQL.With.Rest.Api
{
    /// <summary>
    /// Data access class which is not dependent on graphql in anyway.
    /// </summary>
    public interface IDataRepository
    {
        List<UserDetail> GetUserDetails(string email);
        List<CreditCardTransaction> GetCreditCardTransactions(string email);
    }
    public class DataRepository:IDataRepository
    {
        public List<CreditCardTransaction> GetCreditCardTransactions(string email)
        {
            var generator = new RandomGenerator();
           
            var transactions = Builder<CreditCardTransaction>.CreateListOfSize(50).All()
                .With(a => a.CreditCardNumber = Faker.RandomNumber.Next(11111111, 999999999).ToString())
                .With(a => a.PurchasedFromCompanyName= Faker.Company.Name())
                .With(a => a.AmountSpent = $"$ {Faker.RandomNumber.Next(100000,99999999)}")
                .With(a => a.TransactionDate = DateTime.Now.AddDays(-generator.Next(1, 90)))
                .Build().ToList();

            return transactions;
        }

        /// <summary>
        /// Return MOCKED DATA FOR DEMO
        /// </summary>
        /// <param name="IsPlatinumUser"></param>
        /// <returns></returns>
        public List<UserDetail> GetUserDetails(string email)
        {
            // return mocked data.           
            var daysGenerator = new RandomGenerator();

            // Get it from data source 1
            var addresses = Builder<Address>.CreateListOfSize(50).All()
                .With(a => a.AddressId = RandomNumber.Next())
                .With(a => a.AddressLine1 = Faker.Address.StreetAddress())
                .With(a => a.AddressLine2 = Faker.Address.SecondaryAddress()).Build().ToList();

            // Get it from data source 2
            var creditcards = Builder<CreditCardInfo>.CreateListOfSize(50).All()
                .With(a => a.CreditCardNumber = Faker.RandomNumber.Next(11111111,999999999).ToString())
                .With(a => a.CVVNumber = Faker.RandomNumber.Next(1111,9999).ToString())
                .With(a => a.ExpiryDate = DateTime.Now.AddDays(-daysGenerator.Next(1, 365)).ToShortDateString())
                .With(a => a.DisplayName = Faker.Name.Last())
                .Build().ToList();

            // Get it from data source 3
            var users = Builder<UserDetail>.CreateListOfSize(10).All()
                .With(u => u.FirstName = Faker.Name.First())
                .With(u => u.LastName = Faker.Name.Last())
                .With(u => u.Email = Faker.Internet.Email())
                .With(u => u.IsPlatinumUser = true);

            // merge all three data from different data sources
            var mergedUsers  = users
                     .With(u => u.CreatedDate = DateTime.Now.AddDays(-daysGenerator.Next(1, 365)))
                     .With(u => u.Address = Pick<Address>.RandomItemFrom(addresses))
                     .With(u=> u.CreditCardInfo = Pick<CreditCardInfo>.RandomItemFrom(creditcards))
                     .Build().ToList();

            List<UserDetail> result = new List<UserDetail>();
            if (!string.IsNullOrEmpty(email))
            {
                mergedUsers[0].Email = email;
                result.Add(mergedUsers[0]);
                return result;
            }

            return mergedUsers;
        }
    }

    
}
