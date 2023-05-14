using GraphQL.Types;

namespace GraphQLAPI.Type
{
    public class TaskModelInputType : InputObjectGraphType
    {
        public TaskModelInputType()
        {
            Field<IntGraphType>("id");
            Field<StringGraphType>("title");
            Field<BooleanGraphType>("isCompleted");
            Field<DateTimeGraphType>("dueDate");
            Field<IntGraphType>("categoryId");
        }
    }
}
