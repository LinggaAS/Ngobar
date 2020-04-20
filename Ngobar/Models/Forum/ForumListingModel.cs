namespace Ngobar.Models.Forum
{
    public class ForumListingModel
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public string Deskripsi { get; set; }
        public string ImageUrl { get; set; }

        public int nomorPost { get; set; }
        public int nomorUser { get; set; }
        public bool postBaru { get; set; }
    }
}
