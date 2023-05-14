using BusinessLogic.Models;
using GraphQL.Types;

namespace GraphQLAPI.Type
{
    public class CategoryModelType : ObjectGraphType<CategoryModel>
    {
        public CategoryModelType()
        {
            Field(t => t.Id);
            Field(t => t.Name);
        }
    }
}
