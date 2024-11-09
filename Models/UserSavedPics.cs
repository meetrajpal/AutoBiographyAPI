using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoBiographyAPI.Models
{
    public class UserSavedPics
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(8), MaxLength(50)]
        public required string Username { get; set; }

        [Required]
        public required string ThumbUri { get; set; }

        [Required]
        public required string FullUri { get; set; }

        [Required]
        public string? Slug { get; set; }
    }
}
