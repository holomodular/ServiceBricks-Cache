﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceBricks.Storage.EntityFrameworkCore;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.SqlServer
{
    /// <summary>
    /// IServiceCollection extensions for the Cache Brick.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksCacheSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(CacheSqlServerModule), new CacheSqlServerModule());

            // Add Core service
            services.AddServiceBricksCacheEntityFrameworkCore(configuration);

            //Register Database
            var builder = new DbContextOptionsBuilder<CacheSqlServerContext>();
            string connectionString = configuration.GetSqlServerConnectionString(
                CacheSqlServerConstants.APPSETTING_CONNECTION_STRING);
            builder.UseSqlServer(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().Name);
                x.EnableRetryOnFailure();
            });
            services.Configure<DbContextOptions<CacheSqlServerContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<CacheSqlServerContext>>(builder.Options);
            services.AddDbContext<CacheSqlServerContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // Storage Services
            services.AddScoped<IStorageRepository<CacheData>, CacheStorageRepository<CacheData>>();

            return services;
        }
    }
}