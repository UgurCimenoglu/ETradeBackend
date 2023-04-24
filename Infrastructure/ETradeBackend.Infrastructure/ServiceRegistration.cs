using ETradeBackend.Application.Abstracts.Services;
using ETradeBackend.Application.Abstracts.Services.Configurations;
using ETradeBackend.Application.Abstracts.Storage;
using ETradeBackend.Application.Abstracts.Token;
using ETradeBackend.Infrastructure.Enums;
using ETradeBackend.Infrastructure.Services.Configurations;
using ETradeBackend.Infrastructure.Services.Mail;
using ETradeBackend.Infrastructure.Services.Storage;
using ETradeBackend.Infrastructure.Services.Storage.Azure;
using ETradeBackend.Infrastructure.Services.Storage.LocalStorage;
using ETradeBackend.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeBackend.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IApplicationService, ApplicationService>();
        }
        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection services, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    services.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:
                    break;
                default:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
