using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.API;
using Todo.Core;
using Todo.Data;
using Todo.Service;

var builder = WebApplication.CreateBuilder(args);
var conStr = Environment.GetEnvironmentVariable("ConnectionString");
builder.Services.AddDbContext<TodoContext>(options => options.UseSqlServer(conStr));
builder.Services.AddIdentity<TodoUser, TodoRole>()
    .AddEntityFrameworkStores<TodoContext>()
    .AddDefaultTokenProviders();
builder.Services.RegisterServiceLayer();
builder.Services.AddMemoryCache();
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);

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

app.UseMiddleware<RoleAuthorizationMiddleware>();
app.UseMiddleware<RecentlyVisitedMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
