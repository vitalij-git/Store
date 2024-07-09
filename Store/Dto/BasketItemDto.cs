﻿using System.ComponentModel.DataAnnotations;

namespace API.Dto
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1,double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [Range(1,double.MaxValue, ErrorMessage ="Quantity mus be at least 1")]
        public int Quantity { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
