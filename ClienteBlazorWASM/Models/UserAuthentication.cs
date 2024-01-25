using System.ComponentModel.DataAnnotations;

namespace ClienteBlazorWASM.Models
{
    public class UserAuthentication
    {
        [Required(ErrorMessage="El usuario es obligatorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El password es obligatorio")]
        public string Password { get; set; }
    }
}
