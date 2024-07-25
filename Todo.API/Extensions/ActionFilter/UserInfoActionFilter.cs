using Microsoft.AspNetCore.Mvc.Filters;
using Todo.Core;
public class UserInfoActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Items.TryGetValue("UserId", out var userId))
        {
            foreach (var item in context.ActionArguments.Values)
            {
                if (item is BaseDTO baseDto)
                {
                    baseDto.UserId = userId as int?;
                }
            }
        }

        if (context.HttpContext.Items.TryGetValue("OrganizationId", out var orgId))
        {
            foreach (var item in context.ActionArguments.Values)
            {
                if (item is BaseDTO baseDto)
                {
                    baseDto.OrganizationId = orgId as int?;
                }
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {

    }
}

