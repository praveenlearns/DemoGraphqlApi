using GraphQL.Types;

namespace GraphQL.With.Rest.Api
{
    /// <summary>
    /// Not COvering as of now.
    /// </summary>
    public class TestGraphMutation: ObjectGraphType
    {        
        public TestGraphMutation(DataRepository repo)
        {
            //Field<UserInputType>(
               //"createuser",
               //arguments: new QueryArguments(new QueryArgument<NonNullGraphType<UserInputType>> { Name = "owner" }),
               //resolve: context =>
               //{
               //    var user = context.GetArgument<User>("user");
               //    return repo.CreateUser(user);
               //}
            //);
        }
    }
}
