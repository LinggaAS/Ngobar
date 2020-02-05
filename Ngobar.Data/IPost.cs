using Ngobar.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ngobar.Data
{
    public interface IPost
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFIlteredPosts(string searchQuery);

        Task Add(Post post);
        Task Delete(int id);
        Task EditPostKonten(int id, string newKonten);
    }
}
