using System;
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

        [Required]
        public string Gender { get; set; }

        [Required]
        public string KnownAs { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public UserForRegister(){
            this.Created = DateTime.Now;
            this.LastActive = DateTime.Now;
        }
    }
}