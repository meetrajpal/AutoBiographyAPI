using AutoBiographyAPI.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoBiographyAPI.Models
{
    public class UserProfile
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MinLength(8), MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(25)]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }

        [Required, NotMapped]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Both Passwords Must Match")]
        public string CnfPassword { get; set; }

        public string? PicUri { get; set; }

        [NotMapped]
        public IFormFile? Picture { get; set; }

        [Required, NotMapped]

        [MustBeTrue(ErrorMessage = "You must agree to the terms and conditions.")]
        public bool AcceptTerms { get; set; }
    }
}
