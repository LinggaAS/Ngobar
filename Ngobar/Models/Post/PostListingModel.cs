using Ngobar.Models.Forum;

namespace Ngobar.Models.Post
{
    public class PostListingModel
    {
        public int Id { get; set; }
        public string Judul { get; set; }
        public string NamaPembuat { get; set; }
        public int RatingPembuat { get; set; }
        public string IdPembuat { get; set; }
        public string DatePosted { get; set; }

        public ForumListingModel Forum { get; set; }

        public int JumlahBalasan { get; set; }
    }
}
