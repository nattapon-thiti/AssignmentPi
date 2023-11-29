using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Pi.Interfaces.Repositories.Users;
using Pi.Interfaces.Services.Users;
using Pi.Models.Entities.PI;
using Pi.Repositories.Users;
using Pi.Services.UserServices;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"), // ?api-version = 1.0
        new HeaderApiVersionReader("X-Version"), // Headers > X-Version = 1.0
        new MediaTypeApiVersionReader("ver")); // Headers > Accept = application/json; ver=1.0
});

#region add db connection
builder.Services.AddDbContext<PiContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("PiConnection")), ServiceLifetime.Transient);
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddTransient<IUserRepositories, UsersRepositories>();

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
