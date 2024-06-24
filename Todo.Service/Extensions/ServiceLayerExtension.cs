using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Todo.Core;
using Todo.Data;
using Todo.Data.Repository;
using Todo.Service.Assignment;
using Todo.Service.Board;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper.Internal;
using Todo.Service.Extensions.Map;

namespace Todo.Service
{
    public static class ServiceLayerExtension
    {
        public static IServiceCollection RegisterServiceLayer(this IServiceCollection services)
        {

            #region AutoMapper
            services.AddAutoMapper(cfg => cfg.Internal().MethodMappingEnabled = false, typeof(MapProfile).Assembly);
            #endregion

            #region Sınflar
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<CacheService>();
            #endregion

            #region JWT

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = Environment.GetEnvironmentVariable("JwtIssuer"),
           ValidAudience = Environment.GetEnvironmentVariable("JwtAudience"),
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtKey")))
       };
   });
            #endregion

            return services;
        }
    }
}
