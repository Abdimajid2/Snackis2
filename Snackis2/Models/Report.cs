namespace Snackis2.Models
{
    public class Report
    {
        public Guid Id { get; set; }

        public Guid? PostId { get; set; }

        public string? UserId { get; set; }

        public DateTime? ReportDate { get; set; }

        public Post? Post { get; set; }
    }
}
