using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.DTOs.Configuratiıons;

namespace ETradeBackend.Application.Abstracts.Services.Configurations
{
    public interface IApplicationService
    {
        List<Menu> GetAuthorizationDefinitionEndpoints(Type type);
    }
}
