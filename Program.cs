using AuthServiceApi.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<PermissionsContext>(options => options.UseInMemoryDatabase("AuthServiceApi_test"));
builder.Services.AddDbContext<RolesContext>(options => options.UseInMemoryDatabase("AuthServiceApi_test"));
builder.Services.AddDbContext<UsersContext>(options => options.UseInMemoryDatabase("AuthServiceApi_test"));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
