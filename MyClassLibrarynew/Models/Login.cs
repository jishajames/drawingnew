using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MyClassLibrarynew.Models
{
    public class Login
    {
        public static ClaimsIdentity Identity { get; internal set; }
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

    }
}
