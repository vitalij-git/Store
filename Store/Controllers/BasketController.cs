using API.Dto;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasketById(string id) 
        {
            var customerBasket = await _basketRepository.GetCustomerBasketAsync(id);
            return Ok(customerBasket ?? new CustomerBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateCustomerBasketAsync(CustomerBasketDto customerBasketDto)
        {
            var  customerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasketDto);
            var updatedBasket = await _basketRepository.UpdateCustomerBasketAsync(customerBasket);
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteCustomerBasket(string id)
        {
            await _basketRepository.DeleteCustomerBasketAsync(id);
        }
    }
}
