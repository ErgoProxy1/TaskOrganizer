
namespace TaskOrganizer.API.DTOs
{
    public class UserResponseDTO
    {
        public required string Uid { get; set; }

        public required string DisplayName { get; set; }

        public required string Email { get; set; }
    }
}
