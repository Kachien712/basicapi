using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Interfaces;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CommentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Comment?> GetComment(int id)
        {
            
            var commentModel = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentModel == null)
            {
                return null;
            }
            else
            {
                return commentModel;
            }
        }

        public async Task<List<Comment>> GetComments()
        {
            return await _dbContext.Comments.ToListAsync();
        }

        public async Task<Comment> CreateCommentAsync(Comment commentModel)
        {
            await _dbContext.Comments.AddAsync(commentModel);
            await _dbContext.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, Comment commentModel)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                return null;
            }
            else
            {
                comment.Title = commentModel.Title;
                comment.Content = commentModel.Content;

                await _dbContext.SaveChangesAsync();
                return comment;
            }
        }

        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                return null;
            }
            else
            {
                _dbContext.Remove(comment);
                await _dbContext.SaveChangesAsync();
                return comment;
            }
        }
    }
}
