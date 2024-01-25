using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Models.DTOs
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "EL usuario es obligatorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El password es obligatorio")]
        public string Password { get; set; }
    }
}
