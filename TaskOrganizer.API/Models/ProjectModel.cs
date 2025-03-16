namespace TaskOrganizer.API.Models
{
    public class ProjectModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = "";
        public required string CreatedByUid { get; init; }
    }
}
