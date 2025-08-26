using BlogApplication.Data;
using BlogApplication.Models.Domain;
using BlogApplication.Models.ViewModel;
using BlogApplication.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging;

namespace BlogApplication.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ITagInterface _tagRepository;
        public BlogController(ITagInterface tagRepository, IBlogRepository blogRepository)
        {
            _tagRepository = tagRepository;
            _blogRepository = blogRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await _tagRepository.GetAllAsync();

            var model = new BlogViewModel
            {
                Tags = tags.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BlogViewModel model)
        {
            var blog = new Blog
            {
                Heading = model.Heading,
                PageTitle = model.PageTitle,
                Content = model.Content,
                CreatedAt = DateTime.Now,
                Author = model.Author,
                Visable = model.Visable,
                ImageURL = model.ImageURL,
                Tags = new List<Tag>()
            };

            var tagGuids = model.SelectedTags.Select(Guid.Parse).ToList();
            var existingTags = await _tagRepository.GetAllAsync();
            var tagsToAdd = existingTags.Where(t => tagGuids.Contains(t.Id)).ToList();

            blog.Tags.AddRange(tagsToAdd);

            await _blogRepository.AddAsync(blog);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _blogRepository.GetAllAsync();
            return View(blogs);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            
            var blog = await _blogRepository.GetAsync(id);
            var tagsDomainModel = await _tagRepository.GetAllAsync();

            if (blog != null)
            {
                
                var model = new BlogViewModel
                {
                    Id = blog.Id,
                    Heading = blog.Heading,
                    PageTitle = blog.PageTitle,
                    Content = blog.Content,
                    CreatedAt = blog.CreatedAt,
                    Author = blog.Author,
                    Visable = blog.Visable,
                    ImageURL = blog.ImageURL,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList(),
                    SelectedTags = blog.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(model);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BlogViewModel model)
        {
           
            var blogToUpdate = new Blog
            {
                Id = model.Id,
                Heading = model.Heading,
                PageTitle = model.PageTitle,
                Content = model.Content,
                Author = model.Author,
                Visable = model.Visable,
                ImageURL = model.ImageURL,
                Tags = new List<Tag>()
            };

            
            var selectedTags = new List<Tag>();
            foreach (var selectedTag in model.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tagGuid))
                {
                    var foundTag = await _tagRepository.GetAsync(tagGuid);
                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            blogToUpdate.Tags = selectedTags;

        
            var updatedBlog = await _blogRepository.UpdateAsync(blogToUpdate);

            if (updatedBlog != null)
            {
                return RedirectToAction("Index", new { id = updatedBlog.Id });
            }

            return RedirectToAction("Update", new { id = blogToUpdate.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var blog = await _blogRepository.GetAsync(id);
            if (blog == null)
                return Json(new { success = false, message = "Blog not found" });

            await _blogRepository.DeleteAsync(id);
            return Json(new { success = true, message = "Blog deleted successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var blog = await _blogRepository.GetAsync(id);
            if (blog == null)
                return View(null);
            return View(blog);
        }

    }
}