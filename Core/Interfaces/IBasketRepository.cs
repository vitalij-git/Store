using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetCustomerBasketAsync(string basketId);
        Task<CustomerBasket> UpdateCustomerBasketAsync(CustomerBasket customerBasket);
        Task<bool> DeleteCustomerBasketAsync(string basketId);
    }
}
