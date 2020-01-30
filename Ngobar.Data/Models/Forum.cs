using System;
using System.Collections.Generic;

namespace Ngobar.Data.Models
{
    public class Forum
    {
        public int Id { get; set; }
        public string Judul { get; set; }
        public string Deskripsi { get; set; }
        public DateTime Dibuat { get; set; }
        public string ImageUrl { get; set; }

        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
