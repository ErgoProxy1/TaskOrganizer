namespace TaskOrganizer.API.DTOs
{
    public class ProjectDTO
    {
        public required string Name { get; set; }
        public string Description { get; set; } = "";
        public required string CreatedByUid { get; init; }
    }
}
