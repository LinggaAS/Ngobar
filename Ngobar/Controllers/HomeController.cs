using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Ngobar.Data;
using Ngobar.Data.Models;
using Ngobar.Models;
using Ngobar.Models.Forum;
using Ngobar.Models.Home;
using Ngobar.Models.Post;

namespace Ngobar.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPost _postService;

        public HomeController(IPost postService)
        {
            _postService = postService;
        }

        public IActionResult Index()
        {
            var model = BuildHomeIndexModel();
            return View(model);
        }

        private HomeIndexModel BuildHomeIndexModel()
        {
            var PostTerbaru = _postService.GetPostTerbaru(10);

            var posts = PostTerbaru.Select(post => new PostListingModel
            {
                Id = post.Id,
                Judul = post.Judul,
                IdPembuat = post.User.Id,
                NamaPembuat = post.User.UserName,
                RatingPembuat = post.User.Rating,
                DatePosted = post.Dibuat.ToString(),
                JumlahBalasan = post.balasan.Count(),
                Forum = GetForumListingForPost(post)
            });

            return new HomeIndexModel
            {
                PostTerbaru = posts,
                SearchQuery = ""
            };
        }

        private ForumListingModel GetForumListingForPost(Post post)
        {
            var forum = post.Forum;

            return new ForumListingModel
            {
                Id = forum.Id,
                Nama = forum.Judul,
                ImageUrl = forum.ImageUrl
            };
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
