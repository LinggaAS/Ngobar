using System;
using System.Collections.Generic;
using System.Linq;
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

        public PostController(IPost postService)
        {
            _postService = postService;
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
                Balasan = balasan
            };
            return View(model);
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
                KontenBalasan = balas.Konten
            });
        }
    }
}