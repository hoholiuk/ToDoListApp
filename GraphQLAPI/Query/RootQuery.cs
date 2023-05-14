using GraphQL.Types;

namespace GraphQLAPI.Query
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Field<TaskModelQuery>("taskModelQuery", resolve: context => new { });
            Field<CategoryModelQuery>("categoryModelQuery", resolve: context => new { });
        }
    }
}
