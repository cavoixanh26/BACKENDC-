namespace TaskManagement.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string? Content { get; set; }
    }
}
