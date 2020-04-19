using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ngobar.Data;
using Ngobar.Data.Models;
using Ngobar.Models.Balasan;

namespace Ngobar.Controllers
{
    public class BalasanController : Controller
    {
        private readonly IPost _postService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;

        public BalasanController(IPost postService, 
            UserManager<ApplicationUser> userManager,
            IApplicationUser userService)
        {
            _postService = postService;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IActionResult> Create(int id)
        {
            var post = _postService.GetById(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new BalasanPostModel
            {
                KontenPost = post.Konten,
                JudulPost = post.Judul,
                PostId = post.Id,

                IdPembuat = user.Id,
                NamaAuthor = User.Identity.Name,
                AuthorImageUrl = user.ProfileImageUrl,
                RatingAuthor = user.Rating,
                IsAuthorAdmin = User.IsInRole("Admin"),

                IdForum = post.Forum.Id,
                NamaForum = post.Forum.Judul,
                ForumImageUrl = post.Forum.ImageUrl,

                Dibuat = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> TambahBalasan(BalasanPostModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            var balasan = BuildReply(model, user);

            await _postService.TambahBalasan(balasan);
            await _userService.UpdateUserRating(userId, typeof(PostBalasan));

            return RedirectToAction("Index", "Post", new { id = model.PostId});
        }

        private PostBalasan BuildReply(BalasanPostModel model, ApplicationUser user)
        {
            var post = _postService.GetById(model.PostId);

            return new PostBalasan
            {
                Post = post,
                Konten = model.KontenBalasan,
                Dibuat = DateTime.Now,
                User = user
            };
        }
    }
}