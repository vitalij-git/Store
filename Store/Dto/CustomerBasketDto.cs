using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Dto
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public List<BasketItemDto> BasketItems { get; set; } 
    }
}
