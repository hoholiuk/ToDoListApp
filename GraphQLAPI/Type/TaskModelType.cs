using BusinessLogic.Models;
using GraphQL.Types;

namespace GraphQLAPI.Type
{
    public class TaskModelType : ObjectGraphType<TaskModel>
    {
        public TaskModelType()
        {
            Field(t => t.Id);
            Field(t => t.Title);
            Field(t => t.IsCompleted);
            Field(t => t.DueDate, nullable: true);
            Field(t => t.CategoryId, nullable: true);
        }
    }
}
