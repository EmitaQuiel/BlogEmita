using System.ComponentModel.DataAnnotations;

namespace BlogEmi.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The title is required")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The content is required")]
        public string Content { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public byte[]? Image { get; set; }


    }
}
