using GraphQLAPI.Type;
using BusinessLogic.Repositories;
using GraphQL;
using GraphQL.Types;
using GraphQLAPI.Services;

namespace GraphQLAPI.Query
{
    public class TaskModelQuery : ObjectGraphType
    {
        public TaskModelQuery(IEnumerable<ITaskRepository> taskRepositories, IHttpContextAccessor accessor)
        {
            var _repositorySwitcher = new RepositorySwitcher(taskRepositories);
            ITaskRepository taskRepository = _repositorySwitcher.GetTaskRepository(accessor);

            Field<ListGraphType<TaskModelType>>("tasks",
                resolve: context =>
                {
                    return taskRepository.GetAllTasks();
                });

            Field<TaskModelType>("task",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    return taskRepository.GetById(context.GetArgument<int>("id"));
                });
        }
    }
}
