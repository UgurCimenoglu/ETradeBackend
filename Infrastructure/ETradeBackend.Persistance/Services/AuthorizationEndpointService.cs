using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using ETradeBackend.Application.Abstracts.Services.Configurations;
using ETradeBackend.Application.Repositories.EndpointRepository;
using ETradeBackend.Application.Repositories.MenuRepository;
using ETradeBackend.Domain.Entities;
using ETradeBackend.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistance.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        private readonly IApplicationService _applicationService;
        private readonly IEndpointReadRepository _endpointReadRepository;
        private readonly IEndpointWriteRepository _endpointWriteRepository;
        private readonly IMenuWriteRepository _menuWriteRepository;
        private readonly IMenuReadRepository _menuReadRepository;
        private readonly RoleManager<AppRole> _roleManager;

        public AuthorizationEndpointService(IApplicationService applicationService, IEndpointReadRepository endpointReadRepository, IMenuReadRepository menuReadRepository, IMenuWriteRepository menuWriteRepository, IEndpointWriteRepository endpointWriteRepository, RoleManager<AppRole> roleManager)
        {
            _applicationService = applicationService;
            _endpointReadRepository = endpointReadRepository;
            _menuReadRepository = menuReadRepository;
            _menuWriteRepository = menuWriteRepository;
            _endpointWriteRepository = endpointWriteRepository;
            _roleManager = roleManager;
        }

        public async Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type)
        {
            Menu? _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
            if (_menu == null)
            {
                _menu = new()
                {
                    Id = Guid.NewGuid(),
                    Name = menu
                };
                await _menuWriteRepository.AddAsync(_menu);
                await _menuWriteRepository.SaveAsync();
            }



            Endpoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.Menu)
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            if (endpoint == null)
            {
                var action = _applicationService
                    .GetAuthorizationDefinitionEndpoints(type)
                    .FirstOrDefault(m => m.Name == menu)?.Actions
                    .FirstOrDefault(e => e.Code == code);

                endpoint = new()
                {
                    Id = Guid.NewGuid(),
                    Code = action.Code,
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definition = action.Definition,
                    Menu = _menu
                };

                await _endpointWriteRepository.AddAsync(endpoint);
                await _endpointWriteRepository.SaveAsync();
            }

            foreach (var role in endpoint.Roles)
            {
                endpoint.Roles.Remove(role);
            }

            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Id)).ToListAsync();

            foreach (var role in appRoles)
            {
                endpoint.Roles.Add(role);
            }
            await _endpointWriteRepository.SaveAsync();

        }

        public async Task<List<string>?> GetRolesToEndpointAsync(string code, string menu)
        {
            Endpoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.Roles)
                .Include(e => e.Menu)
                .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            return endpoint?.Roles.Select(r => r.Id).ToList();
        }
    }
}
