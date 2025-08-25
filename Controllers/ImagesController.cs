using BlogApplication.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesRepository imagesRepository;

        public ImagesController(IImagesRepository imagesRepository)
        {
            this.imagesRepository = imagesRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var imageUrl = await imagesRepository.UploadImage(file);
            if (imageUrl == null)
            {
                return Problem(
                       detail: "Something went wrong",
                        statusCode: StatusCodes.Status500InternalServerError
 );
            }
            return new JsonResult(new { imageUrl = imageUrl });
        }
    }
}