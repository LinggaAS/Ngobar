using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Ngobar.Data;
using Ngobar.Data.Models;
using Ngobar.Models.Forum;
using Ngobar.Models.Post;

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
            var posts = forum.Posts;

            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                IdPembuat = post.User.Id,
                RatingPembuat = post.User.Rating,
                NamaPembuat = post.User.UserName,
                Judul = post.Judul,
                DatePosted = post.Dibuat.ToString(),
                JumlahBalasan = post.balasan.Count(),
                Forum = BuildForumListing(post)
            });

            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };

            return View(model);
        }

        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum);
        }

        private ForumListingModel BuildForumListing(Forum forum)
        {
            return new ForumListingModel
            {
                Id = forum.Id,
                Nama = forum.Judul,
                Deskripsi = forum.Deskripsi,
                ImageUrl = forum.ImageUrl
            };
        }
    }
}