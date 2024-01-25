using ClienteBlazorWASM.Models;

namespace ClienteBlazorWASM.Services.IServices
{
    public interface IPostService
    {
        public Task<IEnumerable<Post>> GetPosts();
        public Task<Post> GetPost(int postId);
        public Task<Post> CreatePost(Post post);
        public Task<Post> UpdatePost(int postId, Post post);
        public Task<bool> DeletePost(int postId);
        public Task<string> UploadImage(MultipartFormDataContent content);
 
    }
}
