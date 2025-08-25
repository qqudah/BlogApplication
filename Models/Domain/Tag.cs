namespace BlogApplication.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}