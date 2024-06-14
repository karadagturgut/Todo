using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Todo.Core;
using Todo.Data;
using Todo.Data.Repository;
using Todo.Service.Assignment;
using Todo.Service.Board;

namespace Todo.Service
{
    public static class ServiceLayerExtension
    {
        public static IServiceCollection RegisterServiceLayer(this IServiceCollection services)
        {

            #region AutoMapper
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            #endregion
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<CacheService>();
            return services;
        }
    }
}
