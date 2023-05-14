using GraphQL.Types;

namespace GraphQLAPI.Mutation
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation()
        {
            Field<TaskModelMutation>("taskModelMutation", resolve: context => new { });
            Field<CategoryModelMutation>("categoryModelMutation", resolve: context => new { });
        }
    }
}
