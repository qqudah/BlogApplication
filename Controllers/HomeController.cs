using System.Diagnostics;
using BlogApplication.Models;
using BlogApplication.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogRepository _blogRepository;

        public HomeController(ILogger<HomeController> logger, IBlogRepository blogRepository)
        {
            _logger = logger;
            _blogRepository = blogRepository;
        }

        public async Task<IActionResult> Index()
        { var blogs =await _blogRepository.GetAllAsync();
            return View(blogs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}