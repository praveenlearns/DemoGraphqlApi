using GraphQL.Client;
using GraphQL.Common.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PS.GraphQL.Client
{
    /// <summary>
    /// Wrapper built over GraphQL.Client nuget
    /// It abstracts clients from graphql.client implementation.
    /// </summary>
    public class GraphQlClientHelper 
    {
        #region Variables

        private GraphQLClient _gqlClient;
        private static MediaTypeHeaderValue defaultMediaType = new MediaTypeHeaderValue("application/json");        
        private static JsonSerializerSettings defaultSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        private static GraphQLRequest _gqlRequest;

        #endregion

        #region constructor

        /// <summary>
        /// Initialize graphql client with endpoint and headers.

        /// </summary>       
        public GraphQlClientHelper(string endpoint, List<Tuple<string, string>> headers,  JsonSerializerSettings jsonSerialSetting = null, MediaTypeHeaderValue mediaType=null)
        {
            var options = new GraphQLClientOptions()
            {
                EndPoint =  new Uri(endpoint),
                JsonSerializerSettings = (jsonSerialSetting == null) ? defaultSettings : jsonSerialSetting,
                MediaType = (mediaType == null) ? defaultMediaType : mediaType
            };

            _gqlClient = new GraphQLClient(options);

            foreach(var each in headers)
            {
                _gqlClient.DefaultRequestHeaders.Add(each.Item1, each.Item2);
            }            
        }

        #endregion

        #region GraphQl client implementations

        /// <summary>
        /// Initialize GraphQl Request object to post. 
        /// </summary>
        public GraphQLRequest PrepareSingleQueryRequest(string query, dynamic inputVariables, string operationName = null)
        {
            if(string.IsNullOrEmpty(query) )
                throw new InvalidOperationException("query cannot be null or empty.");

            _gqlRequest = new GraphQLRequest()
            {
                Query = query,
                Variables = inputVariables,
                OperationName = operationName
            };

            return _gqlRequest;
        }       

        /// <summary>
        /// Call API endpoint and return List of TOut based on initialized graphql request
        /// </summary>        
        public async Task<List<TOut>> GetResponseListAsync<TOut>(GraphQLRequest gqlRequest, string outputFieldName)
        {
            if (gqlRequest == null)
                throw new InvalidOperationException("Invalid graphql request.");

            var gqlResponse = await _gqlClient.PostAsync(gqlRequest);
            return gqlResponse.GetDataFieldAs<List<TOut>>(outputFieldName);
        }

        /// <summary>
        /// Call API endpoint and return List of TOut without graphql request.
        /// You should call PrepareRequest method first.
        /// </summary>          
        public async Task<List<TOut>> GetResponseListAsync<TOut>(string outputFieldName)
        {
            if (_gqlRequest == null)
                throw new InvalidOperationException("Invalid graphql request.");

            var gqlResponse = await _gqlClient.PostAsync(_gqlRequest);           
            return gqlResponse.GetDataFieldAs<List<TOut>>(outputFieldName);
        }

        /// <summary>
        /// Call API endpoint and return object of TOut without graphql request.
        /// You should call PrepareRequest method first.
        /// </summary>    
        public async Task<TOut> GetResponseAsync<TOut>(string outputFieldName)
        {
            if (_gqlRequest == null)
                throw new InvalidOperationException("Invalid graphql request.");

            var gqlResponse = await _gqlClient.PostAsync(_gqlRequest);
            return gqlResponse.GetDataFieldAs<TOut>(outputFieldName);
        }

        /// <summary>
        /// Call API endpoint and return List of TOut with initialized graphql request.
        /// </summary>    
        public async Task<TOut> GetResponseAsync<TOut>(GraphQLRequest gqlRequest, string outputFieldName)
        {
            if (gqlRequest == null)
                throw new InvalidOperationException("Invalid graphql request.");

            var gqlResponse = await _gqlClient.PostAsync(gqlRequest);
            return gqlResponse.GetDataFieldAs<TOut>(outputFieldName);
        }

        #endregion

        private void TestClientCode()
        {
            var twoQueries = @"query($email: String) 
            {
                userInfo: getuserdetails(email: $email) 
                {
                    userId
                    firstName
                    surName: lastName
                    email
                    address 
                    {
                        addressLine1
                        addressLine2
                    }
                    creditCardInfo 
                    {
                        creditCardNumber
                        expiryDate
                        cVVNumber
                    }
                }

                transactions: gettransactions
                {
                    creditCardNumber
                    amountSpent
                    transactionDate
                    purchasedFromCompanyName
                }
            }
            ";

            //string endPoint = "";
            //var authToken = "Bearer jw";
            //var headers = new List<Tuple<string, string>>();
            //headers.Add(new Tuple<string, string>("Authorization", authToken));
            //GraphQlClientHelper gqlClient = new GraphQlClientHelper(endPoint, headers);

            //// Input variables
            //string mail = "test@test.com";           
            //dynamic variables = new { email = mail };

            //// Prepare Request
            //gqlClient.PrepareSingleQueryRequest(query, variables);

            //// Call api (choose correct method for expected List or Single object.)
            //var response1 = gqlClient.GetResponseAsync<dynamic>("getuserdetails").Result;

            ////Client Logic with response
            //Console.WriteLine(response);

        }

    }   
}
