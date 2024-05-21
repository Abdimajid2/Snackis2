namespace Snackis2.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        public string? Header { get; set; }

        public string? Text { get; set; }

        public DateTime PostDate { get; set; }

        public string? UserId { get; set; }

        public Guid? KategoryId { get; set; }

        public List<Comment>? Comments { get; set; }
 
    }
}
