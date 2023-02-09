using System.ComponentModel.DataAnnotations;

namespace webapi.AppService.DTO.DTOCheckout
{
    public class CheckoutOrderDTO
    {
        public Guid CustomerId { get; set; }
        [Required, Range(0, 2, ErrorMessage = " Must be 0,1 and 2 only (0 = Pending, 1 = Processed, 2 = Cancelled)")]
        public short Status { get; set; }
    }
}
