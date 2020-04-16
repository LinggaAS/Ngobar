using Microsoft.AspNetCore.Mvc;
using Ngobar.Data;
using Ngobar.Data.Models;
using Ngobar.Models.Forum;
using Ngobar.Models.Post;
using Ngobar.Models.Search;
using System.Linq;

namespace Ngobar.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPost _postService;

        public SearchController(IPost postService)
        {
            _postService = postService;
        }

        public IActionResult Results(string searchQuery)
        {
            var posts = _postService.GetFIlteredPosts(searchQuery);

            var areNoResults = 
                (!string.IsNullOrEmpty(searchQuery) && !posts.Any());

            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                IdPembuat = post.User.Id,
                NamaPembuat = post.User.UserName,
                RatingPembuat = post.User.Rating,
                Judul = post.Judul,
                DatePosted = post.Dibuat.ToString(),
                JumlahBalasan = post.balasan.Count(),
                Forum = BuildForumListing(post)
            });

            var model = new SearchResultModel
            {
                Posts = postListings,
                SearchQuery = searchQuery,
                EmptySearchResults = areNoResults
            };

            return View(model);
        }

        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;

            return new ForumListingModel
            {
                Id = forum.Id,
                ImageUrl = forum.ImageUrl,
                Nama = forum.Judul,
                Deskripsi = forum.Deskripsi
            };
        }

        [HttpPost]
        public IActionResult Search(string searchQuery)
        {
            return RedirectToAction("Results", new { searchQuery });
        }

    }
}