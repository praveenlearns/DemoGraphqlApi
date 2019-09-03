using GraphQL.Types;
using GraphQL.With.Rest.Api.ServiceLayer;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace GraphQL.With.Rest.Api
{
    /// <summary>
    /// It 
    /// </summary>
    public class TestGraphQueryResolver : ObjectGraphType
    {
        public TestGraphQueryResolver(  IGraphQLReaderService graphQLReaderService, 
                                        ILogger<TestGraphQueryResolver> logger, 
                                        IGraphQLAuthResolver resolver)
        {
            // query 1 - for user's data 
            FieldAsync<ListGraphType<UserDetailGraphType>,List<UserDetail>>
                ( name:"getuserdetails",
                 description:null,
                 arguments: new QueryArguments(new QueryArgument<StringGraphType>
                {
                    Name = "email",
                    Description = "user email"
                }),
                resolve: resolver.Resolve(graphQLReaderService.GetUserDetails));

            // query 2 - for user's transactions 
            FieldAsync<ListGraphType<CreditCardTransactionGraphType>, List<CreditCardTransaction>>
                           (name: "gettransactions",
                            description: null,
                            arguments: new QueryArguments(new QueryArgument<StringGraphType>
                            {
                                Name = "email",
                                Description = "user email"
                            }),
                           resolve: resolver.Resolve(graphQLReaderService.GetCreditCardTransactions));
        }

    }
}
