﻿using ETradeBackend.Application.Abstracts;
using ETradeBackend.Persistance.Concretes;
using Microsoft.EntityFrameworkCore;
using ETradeBackend.Persistance.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace ETradeBackend.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ETradeDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
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

        }
    }
}
