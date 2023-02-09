using System.ComponentModel.DataAnnotations;

namespace webapi.AppService.DTO.DTOUser
{
    public class AddUserDTO
    {
        [Required]
        public string? Username { get; set; }
    }
}
