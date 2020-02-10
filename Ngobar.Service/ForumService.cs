﻿using Microsoft.EntityFrameworkCore;
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
            var forum = _context.Forums.Where(f => f.Id == id)
                .Include(f => f.Posts).ThenInclude(p => p.User)
                .Include(f => f.Posts).ThenInclude(p => p.balasan).ThenInclude(r => r.User)
                .FirstOrDefault();

            return forum;
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