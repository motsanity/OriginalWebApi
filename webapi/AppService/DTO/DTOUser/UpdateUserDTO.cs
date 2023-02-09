using System.ComponentModel.DataAnnotations;

namespace webapi.AppService.DTO.DTOUser
{
    public class UpdateUserDTO //added 1:14pm 1/24/2023
    {
        [Required]
        public Guid UserId { get; set; } //added 2:00pm 1/24/2023
        public string? Username { get; set; }
    }
}
