namespace TransactionPerRequest.Api.Models
{
    public class BlogModel
    {
        public BlogModel()
        {
            this.Posts = new List<PostModel>();
        }

        public int Id { get; set; }

        public string? Url { get; set; }

        public List<PostModel>? Posts { get; set; }
    }
}
