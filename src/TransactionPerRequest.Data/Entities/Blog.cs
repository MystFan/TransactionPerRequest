namespace TransactionPerRequest.Data.Entities
{
    public class Blog
    {
        public Blog()
        {
            this.Posts = new List<Post>();
        }

        public int Id { get; set; }

        public string? Url { get; set; }

        public List<Post>? Posts { get; set; }
    }
}