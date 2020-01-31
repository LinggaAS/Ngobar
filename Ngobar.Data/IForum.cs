﻿using Ngobar.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ngobar.Data
{
    public interface IForum
    {
        Forum GetById(int id);
        IEnumerable<Forum> GetAll();
        IEnumerable<ApplicationUser> GetAllActiveUsers();

        Task Create(Forum forum);
        Task Delete(int forumId);
        Task UpdateForumJudul(int forumId, string newJudul);
        Task UpdateDeskripsi(int forumId, string newDeskripsi);
    }
}
