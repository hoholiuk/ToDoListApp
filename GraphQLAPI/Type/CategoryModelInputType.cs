using GraphQL.Types;

namespace GraphQLAPI.Type
{
    public class CategoryModelInputType : InputObjectGraphType
    {
        public CategoryModelInputType()
        {
            Field<IntGraphType>("id");
            Field<StringGraphType>("name");
        }
    }
}
