namespace TaskManagement.Dto
{
    public class SectionDto
    {
        public int Id { get; set; }
        public int WorkSpaceId { get; set; }
        public string Title { get; set; } = null!;
        public string Describe { get; set; } = null!;
        public bool Status { get; set; } = false;
    }
}
