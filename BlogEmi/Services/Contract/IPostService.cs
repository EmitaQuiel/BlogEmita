using BlogEmi.Models;

namespace BlogEmi.Services.Contract
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPosts();
        Task<Post> GetPostById(int id);
        Task<bool> CreatePost(Post post);
        Task UpdatePost(Post post);
        Task DeletePost(int id);
    }
}
