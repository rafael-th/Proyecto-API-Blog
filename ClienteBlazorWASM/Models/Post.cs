using System.ComponentModel.DataAnnotations;

namespace ClienteBlazorWASM.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El título es obligatorio")] public string Title { get; set; }
        [Required(ErrorMessage = "La descripción es obligatoria")] public string Description { get; set; }
        public string? RouteImage { get; set; }
        [Required(ErrorMessage = "Las etiquetas son obligatorias")] public string Tags { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; }
    }
}
