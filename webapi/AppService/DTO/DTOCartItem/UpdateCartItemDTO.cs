using System.ComponentModel.DataAnnotations;

namespace webapi.AppService.DTO.DTOCartItem

{
    public class UpdateCartItemDTO //added 1:14pm 1/24/2023
    {
        [Required]
        public Guid CartItemId { get; set; } //added 2:00pm 1/24/2023
        public string? CartItemName { get; set; } //added 4:33pm 1/24/2023
    }
}
