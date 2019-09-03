using GraphQL.Types;
using GraphQL.Validation.Complexity;
using GraphQL.With.Rest.Api.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GraphQL.With.Rest.Api
{
    [Produces("application/json")]
    [Route("api/GraphQL")]
    public class GraphQLController : ControllerBase
    {
        private readonly IDocumentExecuter _executer;
        // This is resolved by TestGraphSchema class in startup which has info on which class has queries defined.
        private ISchema _schema;
        private readonly IDocumentExecuter _documentExecuter;       
        private readonly ILogger<GraphQLController> _logger;
        private readonly IHttpContextAccessor _context;

        public GraphQLController(IDocumentExecuter documentExecuter, ISchema schema, ILogger<GraphQLController> logger, IHttpContextAccessor context)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
            _logger = logger;
            _context = context;
        }
   
        /// <summary>
        /// Single graphQL endpoint for dynamic contract based queries.
        /// Please reach out to team to get more information on its usage.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            var result = await this.ExecuteGraphQueryAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// Primary method which will invoke right query in Resolver class TestGraphQueryResolver
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private async Task<ExecutionResult> ExecuteGraphQueryAsync(GraphQLQuery query)
        {
            var start = DateTime.UtcNow;
            // SET UP options to resolve and execute query running.
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query?.Query,
                Inputs = query?.Variables.ToInputs(),
                ComplexityConfiguration = new ComplexityConfiguration() { MaxDepth = 15 },
                UserContext = new GraphQLUserContext
                {
                    User = _context.HttpContext.User
                },
                EnableMetrics = true,
                ExposeExceptions= true
            };

            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            //ENABLE TRACING FOR LOGGING or Adding in App Insights
            //if (result != null)
            //{
            //    if (result.Operation != null) { result.EnrichWithApolloTracing(start); }

            //    if (result.Extensions == null) { result.Extensions = new Dictionary<string, object>(); }
            //    // using below values in GraphQlFilter
            //    result.Extensions.Add("inputVariables", query?.Variables);
            //    result.Extensions.Add("inputQuery", query?.Query);
            //}

            return result;
        }

    }
}
