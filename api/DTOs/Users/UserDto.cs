using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Users
{
    public class UserDto
    {
        public int id { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]

        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        public string fullName => $"{firstName} {lastName}";

        public string? avatarUrl { get; set; }

        public string? headerImageUrl { get; set; }

        public string? jobTitle { get; set; }

        public string? organization { get; set; }

        public string? location { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public DateTime? lastLoggedIn { get; set; }

        public bool? isActive { get; set; }
    }
}
