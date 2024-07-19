using Microsoft.EntityFrameworkCore;
using TaskManager.API.Extensions;
using TaskManager.API.Helpers;
using TaskManager.API.Services;
using TaskManager.Dal;
using TaskManager.Dal.Repository;
using TaskManager.DAL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connection, b => b.MigrationsAssembly("TaskManager.Api"));
});

builder.Services.AddJwtAuthentication();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AccountService>();

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
