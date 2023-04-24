using System.Reflection;
using ETradeBackend.Application.Abstracts.Services.Configurations;
using ETradeBackend.Application.CustomAttributes;
using ETradeBackend.Application.DTOs.Configuratiıons;
using ETradeBackend.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Action = ETradeBackend.Application.DTOs.Configurations.Action;

namespace ETradeBackend.Infrastructure.Services.Configurations
{
    public class ApplicationService : IApplicationService
    {
        public List<Menu> GetAuthorizationDefinitionEndpoints(Type type)
        {
            Assembly? assembly = Assembly.GetAssembly(type);
            var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));
            List<Menu> menus = new();
            if (controllers is not null)
            {
                foreach (var controller in controllers)
                {
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                    if (actions is not null)
                    {
                        foreach (var action in actions)
                        {
                            var attributes = action.GetCustomAttributes(true);
                            if (attributes is not null)
                            {
                                Menu menu = null;
                                var authorizeDefinitionAttribute =
                                    attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute))
                                        as AuthorizeDefinitionAttribute;
                                if (!menus.Any(m => m.Name == authorizeDefinitionAttribute.Menu))
                                {
                                    menu = new() { Name = authorizeDefinitionAttribute.Menu };
                                    menus.Add(menu);
                                }
                                else
                                    menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu);

                                Action _action = new()
                                {
                                    ActionType = Enum.GetName(typeof(ActionType), authorizeDefinitionAttribute.ActionType),
                                    Definition = authorizeDefinitionAttribute.Definition,
                                };
                                var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                if (httpAttribute is not null)
                                    _action.HttpType = httpAttribute.HttpMethods.First();
                                else
                                    _action.HttpType = HttpMethods.Get;
                                _action.Code =
                                    $"{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ", "")}";
                                menu.Actions.Add(_action);
                            }

                        }
                    }
                }
            }
            return menus;
        }
    }
}
