using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class UserInfoMiddleware
{
    private readonly RequestDelegate _next;

    public UserInfoMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        var loggedUser = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!string.IsNullOrEmpty(loggedUser) && int.TryParse(loggedUser, out var parsedUserId))
        {
            context.Items["UserId"] = parsedUserId;
        }

        var organizationId = context.User.FindFirst("Organization")?.Value;
        if (!string.IsNullOrEmpty(organizationId) && int.TryParse(organizationId, out var parsedOrganizationId))
        {
            context.Items["OrganizationId"] = parsedOrganizationId;
        }

        await _next(context);
    }
}

