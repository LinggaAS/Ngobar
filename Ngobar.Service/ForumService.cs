using Microsoft.EntityFrameworkCore;
using Ngobar.Data;
using Ngobar.Data.Models;
using System;
using System.Collections.Generic;
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

        public Task Create(Forum forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int forumId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.Forums.Include(forum => forum.Posts);
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public Forum GetById(int id)
        {
            throw new NotImplementedException();
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
