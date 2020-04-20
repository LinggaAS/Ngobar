using Microsoft.EntityFrameworkCore;
using Ngobar.Data;
using Ngobar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ngobar.Service
{
    public class ForumService : IForum
    {
        private readonly ApplicationDbContext _context;

        public ForumService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Forum forum)
        {
            _context.Add(forum);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int forumId)
        {
            var forum = GetById(forumId);
            _context.Remove(forum);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ApplicationUser> GetActiveUsers(int id)
        {
            var posts = GetById(id).Posts;
            
            if (posts != null || !posts.Any())
            { 
                var postUsers = posts.Select(p => p.User);
                var balasanUsers = posts.SelectMany(p => p.balasan).Select(b => b.User);

                return postUsers.Union(balasanUsers).Distinct();
            }

            return new List<ApplicationUser>();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.Forums.Include(forum => forum.Posts);
        }

        public Forum GetById(int id)
        {
            var forum = _context.Forums.Where(f => f.Id == id)
                .Include(f => f.Posts).ThenInclude(p => p.User)
                .Include(f => f.Posts).ThenInclude(p => p.balasan).ThenInclude(r => r.User)
                .FirstOrDefault();

            return forum;
        }

        public bool postBaru(int id)
        {
            const int waktu = 12;
            var window = DateTime.Now.AddHours(-waktu);
            return GetById(id).Posts.Any(post => post.Dibuat > window);
        }

        public Task UpdateDeskripsi(int forumId, string newDeskripsi)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumJudul(int forumId, string newJudul)
        {
            throw new NotImplementedException();
        }
    }
}
