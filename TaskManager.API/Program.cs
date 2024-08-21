using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Extensions;
using TaskManager.Api.Helpers;
using TaskManager.Api.Services;
using TaskManager.Dal;
using TaskManager.Dal.Repository;
using TaskManager.DAL.EF.Repository;
using TaskManager.DAL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connection);
});

builder.Services.AddJwtAuthentication();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Project>, ProjectRepository>();
builder.Services.AddScoped<IRepository<Desk>, DeskRepository>();
builder.Services.AddScoped<IRepository<WorkTask>, TaskRepository>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IDeskService, DeskService>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTaskManagerSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManager.WebAPI v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
