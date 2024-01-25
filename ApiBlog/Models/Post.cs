using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Models
{
    public class Post
    {
        [Key] public int Id { get; set; }
        [Required]public string Title { get; set; }
        [Required] public string Description { get; set; }
        public string? RouteImage { get; set; }
        [Required] public string Tags { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; }
    }
}
