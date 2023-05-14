using BusinessLogic.Models;
using BusinessLogic.Repositories;
using GraphQL;
using GraphQL.Types;
using GraphQLAPI.Services;
using GraphQLAPI.Type;

namespace GraphQLAPI.Mutation
{
    public class TaskModelMutation : ObjectGraphType
    {
        public TaskModelMutation(IEnumerable<ITaskRepository> taskRepositories, IHttpContextAccessor accessor)
        {
            var _repositorySwitcher = new RepositorySwitcher(taskRepositories);
            ITaskRepository taskRepository = _repositorySwitcher.GetTaskRepository(accessor);

            Field<TaskModelType>("createTask",
                arguments: new QueryArguments(new QueryArgument<TaskModelInputType> { Name = "task" }),
                resolve: context =>
                {
                    return taskRepository.Create(context.GetArgument<TaskModel>("task"));
                });

            Field<TaskModelType>("updateTask",
                arguments: new QueryArguments(new QueryArgument<TaskModelInputType> { Name = "task" }),
                resolve: context =>
                {
                    return taskRepository.Update(context.GetArgument<TaskModel>("task"));
                });

            Field<BooleanGraphType>("completeTask",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    var taskId = context.GetArgument<int>("id");
                    taskRepository.Complete(taskId);
                    return true;
                });

            Field<BooleanGraphType>("deleteTask",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    var taskId = context.GetArgument<int>("id");
                    taskRepository.Delete(taskId);
                    return true;
                });
        }
    }
}
