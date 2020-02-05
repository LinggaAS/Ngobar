using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Ngobar.Data;
using Ngobar.Models.Forum;

namespace Ngobar.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;

        public ForumController(IForum forumService)
        {
            _forumService = forumService;
        }

        public IActionResult Index()
        {
            var forums = _forumService.GetAll()
                .Select(forum => new ForumListingModel {
                    Id = forum.Id,
                    Nama = forum.Judul,
                    Deskripsi = forum.Deskripsi
            });

            var model = new ForumIndexModel
            {
                ForumList = forums
            };

            return View(model);
        }

        public IActionResult Topic(int id)
        {
            var forum = _forumService.GetById(id);
            var posts = _postService.GetFilteredPosts(id);

            var postListings = ...
        }
    }
}