using WebApp;
using BusinessLogic.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITaskRepository, MSQL.Repositories.TaskRepository>();
builder.Services.AddScoped<ICategoryRepository, MSQL.Repositories.CategoryRepository>();
builder.Services.AddScoped<ITaskRepository, XML.Repositories.TaskRepository>();
builder.Services.AddScoped<ICategoryRepository, XML.Repositories.CategoryRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDoList}/{action=Index}");

app.Run();
