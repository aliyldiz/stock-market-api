using Microsoft.EntityFrameworkCore;
using webAPI.Data;
using webAPI.Interfaces;
using webAPI.Models;

namespace webAPI.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments.Include(a => a.AppUser).ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
    {
        var comment = _context.Comments.Find(id);
        if (comment == null)
        {
            return null;
        }
        comment.Title = commentModel.Title;
        comment.Content = commentModel.Content;
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comments = await _context.Comments.FindAsync(id);
        if (comments == null)
        {
            return null;
        }

        _context.Comments.Remove(comments);
        await _context.SaveChangesAsync();
        return comments;
    }
}