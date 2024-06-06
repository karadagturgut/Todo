using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class GeneralMiddleware
{
    private readonly RequestDelegate _next;

    public GeneralMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Önceki değerleri alıyoruz:
        var cookieValue = context.Request.Cookies["RecentlyVisited"];
        List<string> pathList;

        if (!string.IsNullOrEmpty(cookieValue))
        {
            pathList = JsonSerializer.Deserialize<List<string>>(cookieValue);
        }
        else
        {
            pathList = new List<string>();
        }

        // Context üzerinden gidilen url'i alıyoruz:
        var currentPath = context.Request.Path.ToString();

        // '/' değerlerine bölüp, sonda kalan değeri alıyoruz. Assignment/GetAll gibi bir değer yerine
        // GetAll gibi bir değer gösteriyoruz:
        var segments = currentPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var lastSegment = segments.Length > 0 ? segments[^1] : currentPath;

        if (!pathList.Contains(lastSegment))
        {
            pathList.Add(lastSegment);
            if (pathList.Count > 10)
            {
                pathList.RemoveAt(0);
            }
        }

        // Güncellenmiş listeyi, JSON formatında cookie'ye yazıyoruz:
        var updatedCookieValue = JsonSerializer.Serialize(pathList);

        context.Response.Cookies.Append("RecentlyVisited", updatedCookieValue, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(10)
        });

        await _next(context);
    }
}
