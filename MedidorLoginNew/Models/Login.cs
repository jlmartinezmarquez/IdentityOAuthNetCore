using System.ComponentModel.DataAnnotations;

namespace MedidorLoginNew.Models
{
    public class Login
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
