using System.ComponentModel.DataAnnotations;

namespace webapi.AppService.DTO.DTOCartItem //added 4:06pm 1/24/2023
{
    public class AddCartItemDTO
    {
        [Required]
        public string? CartItemName { get; set; }
        public Guid CustomerId { get; set; }
    }
}
