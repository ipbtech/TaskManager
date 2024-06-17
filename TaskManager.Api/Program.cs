using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Services;
using TaskManager.API.Services.Helpers;
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

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
