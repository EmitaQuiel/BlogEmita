using BlogEmi.Models;
using BlogEmi.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace BlogEmi.Controllers
{
        public class PostController : Controller
        {
            private readonly IPostService _postService;

            public PostController(IPostService postService)
            {
                _postService = postService;
            }

            public async Task<IActionResult> Post()
            {
                var posts = await _postService.GetAllPosts();
                return View(posts);
            }

            public IActionResult Create()
            {
                return View();
            }

        [HttpPost]
        public async Task<IActionResult> Create(Post post, IFormFile? image)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            try
            {
                if (image != null && image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        post.Image = memoryStream.ToArray();  // Guardar imagen como byte[]
                    }
                }

                await _postService.CreatePost(post);
                TempData["Mensaje"] = "Post created successfully.";
                return RedirectToAction("Post");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating post: {ex.Message}");
            }

            return View(post);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postService.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post, IFormFile? image)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            try
            {
                // Si se carga una nueva imagen, la procesamos
                if (image != null && image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        post.Image = memoryStream.ToArray();  // Guardar imagen como byte[]
                    }
                }

                await _postService.UpdatePost(post);
                TempData["Mensaje"] = "Post updated successfully.";
                return RedirectToAction("Post");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating post: {ex.Message}");
            }

            return View(post);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _postService.DeletePost(id);
                TempData["Mensaje"] = "Post deleted successfully.";
                return RedirectToAction("Post");
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = $"Error deleting post: {ex.Message}";
                return RedirectToAction("Post");
            }
        }


    }
}

