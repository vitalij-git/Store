using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BasketRepository 
    {
        private readonly StoreDbContext _context;

        public BasketRepository(StoreDbContext context)
        {
            _context = context;
        }

        public Task<bool> DeleteCustomerBasketAsync(string basketId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerBasket> GetCustomerBasketAsync(string basketId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerBasket> UpdateCustomerBasketAsync(CustomerBasket customerBasket)
        {
            throw new NotImplementedException();
        }
    }
}
