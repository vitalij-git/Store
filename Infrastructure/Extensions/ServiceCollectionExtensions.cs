using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Docker;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBasketRepository, BasketDockerRepository>();
            services.AddScoped<ITokenService, TokenService>();
        
            return services;
        }
    }
}
