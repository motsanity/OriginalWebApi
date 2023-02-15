using System.ComponentModel.DataAnnotations;

namespace webapi.AppService.DTO.DTOCheckout
{
    public class CheckoutOrderDTO
    {
        [Required]
        public Guid CustomerId { get; set; }
        [Required, Range(1, 2, ErrorMessage = " Must be only 1 and 2 only (1 = Processed, 2 = Cancelled)")]
        public short Status { get; set; }
    }
}
