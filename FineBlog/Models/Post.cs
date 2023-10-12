namespace FineBlog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? title { get; set; }
        public string? ShortDescription { get; set; }
        //relation
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? Description { get; set; }
        public string? Slug { get; set; }
    }
}
