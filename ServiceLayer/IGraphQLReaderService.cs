using GraphQL.Types;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQL.With.Rest.Api.ServiceLayer
{
    /// <summary>
    /// Service class containing business logic. 
    /// </summary>
    public interface IGraphQLReaderService
    {
        Task<List<UserDetail>> GetUserDetails(ResolveFieldContext<object> context);
        Task<List<CreditCardTransaction>> GetCreditCardTransactions(ResolveFieldContext<object> context);
    }

    public class GraphQLReaderService : IGraphQLReaderService
    {
        private readonly IDataRepository repo;
        private readonly IConfiguration configuration;

        public GraphQLReaderService(IDataRepository repo)
        {
            this.repo = repo;
        }

        public Task<List<CreditCardTransaction>> GetCreditCardTransactions(ResolveFieldContext<object> context)
        {
            var email = context.GetArgument<string>("email");
            return Task.FromResult(repo.GetCreditCardTransactions(email));
        }

        public Task<List<UserDetail>> GetUserDetails(ResolveFieldContext<object> context)
        {
            var email = context.GetArgument<string>("email");
            return Task.FromResult(repo.GetUserDetails(email));
        }
    }
}
