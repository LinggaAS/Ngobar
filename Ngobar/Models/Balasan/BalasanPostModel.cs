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
        public bool IsAuthorAdmin { get; set; }

        public DateTime Dibuat { get; set; }
        public string KontenBalasan { get; set; }

        public int PostId { get; set; }
        public string JudulPost { get; set; }
        public string KontenPost { get; set; }

        public string NamaForum { get; set; }
        public string ForumImageUrl { get; set; }
        public int IdForum { get; set; }
    }
}
