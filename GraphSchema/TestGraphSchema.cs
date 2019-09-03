using GraphQL;
using GraphQL.Types;

namespace GraphQL.With.Rest.Api
{
    public class TestGraphSchema : Schema
    {
        public TestGraphSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<TestGraphQueryResolver>();
            // not covered in this example. Mutations are nothing but Queries which will write and return the modified entities as well.
            //Mutation = resolver.Resolve<TestGraphMutation>();
        }
    }
}
