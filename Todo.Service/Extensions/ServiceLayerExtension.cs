using Amazon.S3;
using Amazon;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Todo.Core;
using Todo.Data;
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



            #region Sınıflar
            var awsOptions = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("AWS_REGION"))
            };

            services.AddSingleton<IAmazonS3>(sp =>
            {
                return new AmazonS3Client(
                    Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID"),
                    Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY"),
                    awsOptions);
            });

           
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<FileService>();

            var serviceTypes = Assembly.GetExecutingAssembly().GetTypes()
                        .Where(type => type.Name.EndsWith("Service")
                                       && type.IsClass
                                       && !type.IsAbstract
                                       && type.GetInterfaces().Any());

            foreach (var type in serviceTypes)
            {
                foreach (var interfaceType in type.GetInterfaces())
                {
                    services.AddScoped(interfaceType, type);
                }
            }

            services.AddScoped<CacheService>();
            #endregion

            #region JWT

            services.AddIdentity<TodoUser, TodoRole>()
           .AddEntityFrameworkStores<TodoContext>()
           .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
   .AddJwtBearer(options =>
   {
       options.RequireHttpsMetadata = true;
       options.SaveToken = true;
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
   }).AddGoogle(google =>
           {
               google.ClientId = Environment.GetEnvironmentVariable("GoogleClientId");
               google.ClientSecret = Environment.GetEnvironmentVariable("GoogleClientSecret");
           });
            #endregion

            return services;
        }
    }
}
