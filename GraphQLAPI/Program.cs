using BusinessLogic.Repositories;
using GraphQLAPI.Mutation;
using GraphQLAPI.Schema;
using GraphQLAPI.Query;
using GraphQLAPI.Type;
using GraphQL.Server;
using GraphQL.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITaskRepository, MSQL.Repositories.TaskRepository>();
builder.Services.AddScoped<ICategoryRepository, MSQL.Repositories.CategoryRepository>();
builder.Services.AddScoped<ITaskRepository, XML.Repositories.TaskRepository>();
builder.Services.AddScoped<ICategoryRepository, XML.Repositories.CategoryRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<TaskModelType>();
builder.Services.AddScoped<CategoryModelType>();

builder.Services.AddScoped<TaskModelQuery>();
builder.Services.AddScoped<CategoryModelQuery>();
builder.Services.AddScoped<RootQuery>();

builder.Services.AddScoped<TaskModelMutation>();
builder.Services.AddScoped<CategoryModelMutation>();
builder.Services.AddScoped<RootMutation>();

builder.Services.AddScoped<TaskModelInputType>();
builder.Services.AddScoped<CategoryModelInputType>();

builder.Services.AddScoped<ISchema, RootSchema>();

builder.Services.AddGraphQL(options =>
{
    options.EnableMetrics = false;
}).AddSystemTextJson();

var app = builder.Build();

app.UseGraphQL<ISchema>();

app.Run();
