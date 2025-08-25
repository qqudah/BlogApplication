using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApplication.Models.ViewModel
{
    public class BlogViewModel
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Author { get; set; }
        public bool Visable { get; set; }
        public string ImageURL { get; set; }


        public IEnumerable<SelectListItem> Tags { get; set; }

        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
