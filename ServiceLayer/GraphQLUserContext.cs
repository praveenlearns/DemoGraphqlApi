using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GraphQL.With.Rest.Api.ServiceLayer
{
    /// <summary>
    /// Needed to pass on user context.
    /// </summary>
    public class GraphQLUserContext : IProvideClaimsPrincipal
    {
        public ClaimsPrincipal User { get; set; }
    }

    public interface IProvideClaimsPrincipal
    {
        ClaimsPrincipal User { get; }

    }
}
