using Newtonsoft.Json.Linq;

namespace GraphQL.With.Rest.Api
{
    public class GraphQLQuery
    {        
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}
