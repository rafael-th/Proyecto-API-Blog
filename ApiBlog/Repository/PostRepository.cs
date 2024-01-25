using ApiBlog.Data;
using ApiBlog.Models;
using ApiBlog.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiBlog.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _db;
        public PostRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreatePost(Post post)
        {
            post.CreationDate = DateTime.Now;
            _db.Post.Add(post);
            return Save();
        }

        public bool DeletePost(Post post)
        {
            _db.Post.Remove(post);
            return Save();
        }

        public bool ExistPost(string title)
        {                                //convierte a minus y corta los espacios
            return _db.Post.Any(c => c.Title.ToLower().Trim() == title.ToLower().Trim());
        }

        public bool ExistPost(int id)
        {
            return _db.Post.Any(c => c.Id == id);
        }

        public Post GetPost(int postId)
        {
            return _db.Post.FirstOrDefault(c => c.Id == postId);
        }

        public ICollection<Post> GetPosts()
        {
            return _db.Post.OrderBy(c => c.Id).ToList();
        }

        public bool Save()
        {
            //si hay mas de un registro true si no false
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdatePost(Post post)
        {
            post.UpdateDate = DateTime.Now;
            var ImageFromDb = _db.Post.AsNoTracking().FirstOrDefault(x => x.Id == post.Id);
            if (post.RouteImage == null)//si no se subio ningun archivo en el update
            {
                //la ruta de la img va a ser la misma que ya esta en la DB
                post.RouteImage = ImageFromDb.RouteImage;
            }
            _db.Post.Update(post);
            return Save();
        }
    }
}
