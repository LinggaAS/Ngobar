using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ngobar.Data;
using Ngobar.Data.Models;
using Ngobar.Models.Balasan;
using Ngobar.Models.Post;

namespace Ngobar.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IForum _forumService;
        private readonly IApplicationUser _userService;

        private static UserManager<ApplicationUser> _userManager;

        public PostController(IPost postService, 
            IForum forumService, 
            UserManager<ApplicationUser> userManager,
            IApplicationUser userService)
        {
            _postService = postService;
            _forumService = forumService;
            _userManager = userManager;
            _userService = userService;
        }

        public IActionResult Index(int id)
        {
            var post = _postService.GetById(id);

            var balasan = BuildPostBalasan(post.balasan);

            var model = new PostIndexModel
            {
                Id = post.Id,
                Judul = post.Judul,
                IdPembuat = post.User.Id,
                NamaPembuat = post.User.UserName,
                ImgUrlPembuat = post.User.ProfileImageUrl,
                RatingPembuat = post.User.Rating,
                Dibuat = post.Dibuat,
                KontenPost = post.Konten,
                Balasan = balasan,
                ForumId = post.Forum.Id,
                NamaForum = post.Forum.Judul,
                IsAuthorAdmin = IsAuthorAdmin(post.User)
            };
            return View(model);
        }

        public IActionResult Create(int id)
        {
            // id adalah forum.Id
            var forum = _forumService.GetById(id);

            var model = new NewPostModel
            {
                NamaForum = forum.Judul,
                ForumId = forum.Id,
                ForumImageUrl = forum.ImageUrl,
                NamaPembuat = User.Identity.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> TambahPost(NewPostModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var post = BuildPost(model, user);

            await _postService.Add(post);

            // bikin user rating management
            await _userService.UpdateUserRating(userId, typeof(Post));

            return RedirectToAction("Index", "Post", new { id = post.Id });
        }

        private bool IsAuthorAdmin(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user)
                .Result.Contains("Admin");
        }

        private Post BuildPost(NewPostModel model, ApplicationUser user)
        {
            var forum = _forumService.GetById(model.ForumId);
            return new Post
            {
                Judul = model.Judul,
                Konten = model.Konten,
                Dibuat = DateTime.Now,
                User = user,
                Forum = forum
            };
        }

        private IEnumerable<BalasanPostModel> BuildPostBalasan(IEnumerable<PostBalasan> balasan)
        {
            return balasan.Select(balas => new BalasanPostModel
            {
                Id = balas.Id,
                NamaAuthor = balas.User.UserName,
                IdPembuat = balas.User.Id,
                AuthorImageUrl = balas.User.ProfileImageUrl,
                RatingAuthor = balas.User.Rating,
                Dibuat = balas.Dibuat,
                KontenBalasan = balas.Konten,
                IsAuthorAdmin = IsAuthorAdmin(balas.User)
            });
        }
    }
}