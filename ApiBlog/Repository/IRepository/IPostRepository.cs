using ApiBlog.Models;

namespace ApiBlog.Repository.IRepository
{
    public interface IPostRepository
    {
        ICollection<Post> GetPosts();
        Post GetPost(int postId);
        bool ExistPost(string title);
        bool ExistPost(int id);
        bool CreatePost(Post post);
        bool UpdatePost(Post post);
        bool DeletePost(Post post);
        bool Save();
    }
}
