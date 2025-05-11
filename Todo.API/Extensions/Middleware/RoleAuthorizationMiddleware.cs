using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Todo.Core;

public class RoleAuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RoleAuthorizationMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
            var boardService = scope.ServiceProvider.GetRequiredService<IBoardService>();

            var path = context.Request.Path.Value;

            #region public endpoint kontrolü
            var actionRoleEntries = authService.GetActionRolesByPath(path);
            if (actionRoleEntries.Any(ar => ar.IsPublic))
            {
                await _next(context);
                return;
            }
            #endregion

            #region kullanıcı login oldu mu?

            if (!context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Kimlik doğrulaması yapılmamış.");
                return;
            }
            #endregion

            #region kullanıcı yetkili mi?
            var userRoles = context.User.Claims
        .Where(c => c.Type == ClaimTypes.Role)
        .Select(c => c.Value)
        .ToList();


            var isAuthorized = await authService.Authorize(new() { Path = path , Roles = userRoles });

            if (!isAuthorized.IsSuccess)
            {
                context.Response.StatusCode = isAuthorized.StatusCode;
                await context.Response.WriteAsync(isAuthorized.Message!);
                return;
            }
            #endregion

            await _next(context);
        }
    }
}
