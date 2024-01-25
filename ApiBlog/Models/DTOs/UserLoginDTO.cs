using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage ="El usuario es obligatorio")] 
        public string UserName { get; set; }
        [Required(ErrorMessage = "El password es obligatorio")] 
        public string Password { get; set; }
    }
}
