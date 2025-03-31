using Microsoft.AspNetCore.Mvc;
using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.ViewModels.Post;

namespace FitnessPlanMVC.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index(int pageSize = 10, int pageNo = 1, string searchString = "")
        {
            var posts = _postService.GetAllPostForList(pageSize, pageNo, searchString);
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            var postDetail = _postService.GetPostDetail(id);
            if (postDetail == null)
                return NotFound();

            return View(postDetail);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new NewPostVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewPostVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var id = _postService.AddPost(model);
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _postService.DeletePost(id);
            return RedirectToAction(nameof(Index));
        }
    }
}