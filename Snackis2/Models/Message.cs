namespace Snackis2.Models
{
    public class Message
    {
        public Guid Id { get; set; }

        public string? Text { get; set; }

        public string? SendFrom { get; set; }

        public string? SendTo { get; set; }

        public DateTime? SendDate { get; set; }
    }
    
}
