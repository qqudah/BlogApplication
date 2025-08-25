namespace BlogApplication.Models.Domain

{
    public class Blog
    {
        public Guid Id { get; set; }

        public string Heading { get; set; }

        public string PageTitle { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Author { get; set; }

        public bool Visable { get; set; }

        public string ImageURL { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}