using BlogApplication.Models.Domain;
using BlogApplication.Models.ViewModel;
using BlogApplication.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagInterface _tagRepository;

        public TagController(ITagInterface tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_AddPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(TagViewModel tagViewModel)
        {
            if (!ModelState.IsValid) return View(tagViewModel);

            var tag = new Tag { Name = tagViewModel.Name };
            await _tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        public async Task<IActionResult> List()
        {
            var tags = await _tagRepository.GetAllAsync();
            return View(tags);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var tag = await _tagRepository.GetAsync(id);
            if (tag == null) return NotFound();

            var tagViewModel = new TagViewModel
            {
                Id = tag.Id,
                Name = tag.Name
            };
            return View(tagViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TagViewModel tagViewModel)
        {
            if (!ModelState.IsValid) return View(tagViewModel);

            var tag = new Tag
            {
                Id = tagViewModel.Id,
                Name = tagViewModel.Name
            };

            var updated = await _tagRepository.UpdateAsync(tag);
            if (updated == null) return NotFound();

            return RedirectToAction("List");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _tagRepository.DeleteAsync(id);
            if (deleted == null) return NotFound();

            return Ok();
        }
    }
}
