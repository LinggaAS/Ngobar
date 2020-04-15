using System;

namespace Ngobar.Models.Balasan
{
    public class BalasanPostModel
    {
        public int Id { get; set; }
        public string IdPembuat { get; set; }
        public string NamaAuthor { get; set; }
        public int RatingAuthor { get; set; }
        public string AuthorImageUrl { get; set; }
        public DateTime Dibuat { get; set; }
        public string KontenBalasan { get; set; }
        public bool IsAuthorAdmin { get; set; }

        public int PostId { get; set; }
    }
}
