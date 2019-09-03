using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GraphQL.With.Rest.Api.ServiceLayer
{
    public interface IGraphQLAuthResolver
    {
        Func<ResolveFieldContext<object>, Task<T>> Resolve<T>(Func<ResolveFieldContext<object>, Task<T>> method);
    }

    public interface ICustomAuthService
    {
        bool IsAuthorized(ClaimsPrincipal claimPrincipal, object paramToAuthorize, string[] permissions);
    }

    public class CustomAuthService : ICustomAuthService
    {
        public CustomAuthService()
        {

        }
        public bool IsAuthorized(ClaimsPrincipal claimPrincipal, object paramToAuthorize, string[] permissions)
        {
            // Implement youor auth logic. change the class defintion as you wish.

            return true;
        }
    }

    public class GraphQLAuthResolver : IGraphQLAuthResolver
    {
        private readonly ICustomAuthService _authService;

        public GraphQLAuthResolver(ICustomAuthService authService)
        {
            _authService = authService;
        }
        public Func<ResolveFieldContext<object>, Task<T>> Resolve<T>(Func<ResolveFieldContext<object>, Task<T>> method)
        {
            return async context =>
            {
                try
                {
                    //var authInfo = read arguments to authorize like email or anyid etc. from  (context.Arguments);
                    //var permissions = read permissions you are checking like "read/write etc from context.FieldDefinition.GetAuthorizationPermissions().ToArray() ?? null;
                    // Example here will pass null but you need to implement it
                    var authorised = _authService.IsAuthorized(null, null, null);
                    if (!authorised)
                    {
                        //TODO: Fix exception code
                        throw new Exception("You are not authorized to access this data"); 
                    }
                    return await method(context);
                }
                catch (Exception ex)
                {
                    //Add exception logging here
                    var error = new ExecutionError(ex.Message, ex);
                    throw error;
                }
            };
        }
    }
}
