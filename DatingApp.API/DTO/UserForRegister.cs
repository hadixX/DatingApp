using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTO
{
    public class UserForRegister
    {
        [Required]
        public string username { get; set; }
        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="password must be between 4 and 8 char")]
        public string password { get; set; }
    }
}