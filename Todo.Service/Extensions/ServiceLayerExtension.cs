using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Todo.Data.Repository;
using Todo.Data;
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
