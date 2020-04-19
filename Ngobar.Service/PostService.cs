using Microsoft.EntityFrameworkCore;
using Ngobar.Data;
using Ngobar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ngobar.Service
{
    public class PostService : IPost
    {
        private readonly ApplicationDbContext _context;


        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task TambahBalasan(PostBalasan balasan)
        {
            _context.Balasan.Add(balasan);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostKonten(int id, string newKonten)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts
                .Include(post => post.User)
                .Include(post => post.balasan).ThenInclude(balas => balas.User)
                .Include(post => post.Forum);
        }

        public Post GetById(int id)
        {
            return _context.Posts.Where(post => post.Id == id)
                .Include(post => post.User)
                .Include(post => post.balasan).ThenInclude(balas => balas.User)
                .Include(post => post.Forum)
                .First();
        }

        public IEnumerable<Post> GetFIlteredPosts(Forum forum, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery) 
                ? forum.Posts 
                : forum.Posts.Where(post 
                    => post.Judul.Contains(searchQuery) 
                    || post.Konten.Contains(searchQuery));
        }

        public IEnumerable<Post> GetFIlteredPosts(string searchQuery)
        {
            return GetAll().Where(post 
                    => post.Judul.Contains(searchQuery)
                    || post.Konten.Contains(searchQuery));
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            return _context.Forums
                .Where(forum => forum.Id == id).First()
                .Posts;
        }

        public IEnumerable<Post> GetPostTerbaru(int n)
        {
            return GetAll().OrderByDescending(post => post.Dibuat).Take(n);
        }
    }
}
