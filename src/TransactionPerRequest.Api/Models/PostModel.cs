namespace TransactionPerRequest.Api.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int BlogId { get; set; }
    }
}
