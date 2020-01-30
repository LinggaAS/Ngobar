using System;

namespace Ngobar.Data.Models
{
    public class PostBalasan
    {
        public int Id { get; set; }
        public string Konten { get; set; }
        public DateTime Dibuat { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Post Post { get; set; }
    }
}
