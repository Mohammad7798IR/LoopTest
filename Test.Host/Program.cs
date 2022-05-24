using Microsoft.EntityFrameworkCore;
using Test.Host.Repositories.Implementations;
using Test.Host.Repositories.Interfaces;
using Test.Host.Services.Implementations;
using Test.Host.Services.Interfaces;
using TestProject.Host.Context;

var builder = WebApplication.CreateBuilder(args);
IConfiguration _configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region IoC
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

#endregion



builder.Services.AddDbContext<TestDBContext>(options =>
{
    options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
