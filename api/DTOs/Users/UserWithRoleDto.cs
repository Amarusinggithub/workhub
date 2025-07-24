namespace api.DTOs.Users
{
    public class UserWithRolesDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string? AvatarUrl { get; set; }

        public string? JobTitle { get; set; }

        public string? Organization { get; set; }

        public string? Location { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastLoggedIn { get; set; }

        public bool? IsActive { get; set; }

        public List<string> Roles { get; set; } = new();
    }
}
