namespace Snackis2.Models
{
    public class Comment
    {

        public Guid Id { get; set; }

        public string? CommentText { get; set; }

        public string? UserId { get; set; }

        public Guid? PostId { get; set; }

        public DateTime? CommentDate { get; set; }
    }
}
