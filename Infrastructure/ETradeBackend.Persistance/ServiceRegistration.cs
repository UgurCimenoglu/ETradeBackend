using ETradeBackend.Application.Abstracts.Services;
using ETradeBackend.Application.Abstracts.Services.Authhentications;
using ETradeBackend.Application.Repositories.Basket;
using Microsoft.EntityFrameworkCore;
using ETradeBackend.Persistance.Contexts;
using Microsoft.Extensions.DependencyInjection;
using ETradeBackend.Application.Repositories.CustomerRepository;
using ETradeBackend.Persistance.Repositories.CustomerRepository;
using ETradeBackend.Persistance.Repositories.OrderRepository;
using ETradeBackend.Application.Repositories.OrderRepository;
using ETradeBackend.Application.Repositories.ProductRepository;
using ETradeBackend.Persistance.Repositories.ProductRepository;
using ETradeBackend.Application.Repositories.Files;
using ETradeBackend.Persistance.Repositories.FileRepository;
using ETradeBackend.Application.Repositories.ProductImageFileRepository;
using ETradeBackend.Persistance.Repositories.ProductImageFileRepository;
using ETradeBackend.Application.Repositories.InvoiceFileRepository;
using ETradeBackend.Persistance.Repositories.InvoiceFileRepository;
using ETradeBackend.Domain.Entities.Identity;
using ETradeBackend.Persistance.Repositories.BasketRepository;
using ETradeBackend.Persistance.Services;
using ETradeBackend.Persistance.Repositories.BasketItemRepository;
using ETradeBackend.Application.Repositories.BasketItem;
using ETradeBackend.Application.Repositories.CompletedOrder;
using ETradeBackend.Persistance.Repositories.CompletedOrder;
using Microsoft.AspNetCore.Identity;

namespace ETradeBackend.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            //projenin postgre sql veritabanı kullanacağını ve connection stringi burada IoC'ye bildiriyoruz.
            services.AddDbContext<ETradeDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));

            //IoC'ye Identity mekanizmasını kullanacağımızı söylüyoruz ve bunu veritabanına yansıtması için gerekli konfigürasyonu yapıyoruz.
            services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;

                }).AddEntityFrameworkStores<ETradeDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
            services.AddScoped<ICompletedOrderReadRepository, CompletedOrderReadRepository>();
            services.AddScoped<ICompletedOrderWriteRepository, CompletedOrderWriteRepository>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IRoleService, RoleService>();
        }
    }
}
