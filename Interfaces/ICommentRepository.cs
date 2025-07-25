using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetComments();
        public Task<Comment?> GetComment(int id);
        public Task<Comment> CreateCommentAsync(Comment commentModel);
        public Task<Comment?> UpdateCommentAsync(int id, Comment commentModel);
        public Task<Comment?> DeleteCommentAsync(int id);
    }
}
