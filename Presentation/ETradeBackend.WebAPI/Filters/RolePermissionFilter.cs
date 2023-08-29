using System.Reflection;
using ETradeBackend.Application.Abstracts.Services;
using ETradeBackend.Application.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ETradeBackend.WebAPI.Filters
{
    public class RolePermissionFilter : IAsyncActionFilter
    {
        private readonly IUserService _userService;

        public RolePermissionFilter(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var name = context.HttpContext.User.Identity?.Name;

            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            var attribute = descriptor?.MethodInfo
                .GetCustomAttribute(typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;

            if (!string.IsNullOrEmpty(name) && name != "Ugur")
            {
                if (attribute == null)
                {
                    await next();
                }
                var httpAttribute = descriptor?.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute;

                var code = $"{(httpAttribute != null ? httpAttribute.HttpMethods.First() : HttpMethods.Get)}.{attribute?.ActionType}.{attribute?.Definition.Replace(" ", "")}";
                bool hasRole = await _userService.HasRolePermissionToEndpointAsync(name, code);
                if (!hasRole)
                    context.Result = new ObjectResult("Yetkisiz Istek") { StatusCode = 403 };

                else
                    await next();
            }
            else
            {
                if (attribute == null) await next();
                else context.Result = new UnauthorizedResult();
            }
        }
    }
}
