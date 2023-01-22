using ETradeBackend.Application.Abstracts.Storage;
using ETradeBackend.Infrastructure.Enums;
using ETradeBackend.Infrastructure.Services.Storage;
using ETradeBackend.Infrastructure.Services.Storage.Azure;
using ETradeBackend.Infrastructure.Services.Storage.LocalStorage;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeBackend.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
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
