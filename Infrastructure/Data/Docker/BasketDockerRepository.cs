using Core.Interfaces;
using Core.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data.Docker
{
    public class BasketDockerRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketDockerRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteCustomerBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetCustomerBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            if (!data.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<CustomerBasket>(data);
            }
            else
            {
                return null;
            }
        }

        public async Task<CustomerBasket> UpdateCustomerBasketAsync(CustomerBasket customerBasket)
        {
            var updated = await _database.StringSetAsync(customerBasket.Id,
                JsonSerializer.Serialize(customerBasket),
                TimeSpan.FromDays(30));
            if (!updated)
            {
                return null;
            }
            return await GetCustomerBasketAsync(customerBasket.Id);
        }
    }
}
