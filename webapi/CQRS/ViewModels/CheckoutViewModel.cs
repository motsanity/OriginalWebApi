namespace webapi.CQRS.ViewModels
{
    public class CheckoutViewModel
    {
        public Guid CheckoutId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OrderPrimaryId { get; set; }
        public string Status { get; set; }
    }
}                                      
