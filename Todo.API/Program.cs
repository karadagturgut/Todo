using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Todo.Data;
using Todo.Service;

var builder = WebApplication.CreateBuilder(args);
var conStr = Environment.GetEnvironmentVariable("ConnectionString");
builder.Services.AddDbContext<TodoContext>(options => options.UseSqlServer(conStr));
builder.Services.RegisterServiceLayer();
builder.Services.AddMemoryCache();
// Add services to the container.

builder.Services.AddControllers(opt=> opt.Filters.Add<UserInfoActionFilter>()).AddJsonOptions(opt => opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Todo API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer token'ý girin. \r\n\r\n  'Bearer' [boþluk] ardýndan /Auth/Login metodundan aldýðýnýz token'ý yazýnýz.\r\n\r\nÖrnek: \"Bearer ey1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
}); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RoleAuthorizationMiddleware>();
app.UseMiddleware<UserInfoMiddleware>();
app.UseMiddleware<RecentlyVisitedMiddleware>();

app.MapControllers();

app.Run();
