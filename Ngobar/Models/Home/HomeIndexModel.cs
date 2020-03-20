using Ngobar.Models.Post;
using System.Collections.Generic;

namespace Ngobar.Models.Home
{
    public class HomeIndexModel
    {
        public string SearchQuery { get; set; }
        public IEnumerable<PostListingModel> PostTerbaru { get; set; }
    }
}
