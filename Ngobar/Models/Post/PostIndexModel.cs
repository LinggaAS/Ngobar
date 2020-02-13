using Ngobar.Models.Balasan;
using System;
using System.Collections.Generic;

namespace Ngobar.Models.Post
{
    public class PostIndexModel
    {
        public int Id { get; set; }
        public string Judul { get; set; }
        public string IdPembuat { get; set; }
        public string NamaPembuat { get; set; }
        public string ImgUrlPembuat { get; set; }
        public int RatingPembuat { get; set; }
        public DateTime Dibuat { get; set; }
        public string KontenPost { get; set; }

        public IEnumerable<BalasanPostModel> Balasan { get; set; }
    }
}
