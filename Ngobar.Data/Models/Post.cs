using System;
using System.Collections.Generic;
using System.Text;

namespace Ngobar.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Judul { get; set; }
        public string Konten { get; set; }
        public DateTime Dibuat { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Forum Forum { get; set; }

        public virtual IEnumerable<PostBalasan> balasan { get; set; }
    }
}
