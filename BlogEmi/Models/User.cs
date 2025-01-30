using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BlogEmi.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [Required, StringLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required, StringLength(50)]
        public string Email { get; set; } = string.Empty;
        [Required, PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
    }
}
