using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAPI.Data;
using webAPI.Dtos.Comment;
using webAPI.Extensions;
using webAPI.Interfaces;
using webAPI.Mappers;
using webAPI.Models;

namespace webAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;
    private readonly UserManager<AppUser> _userManager;
    
    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, UserManager<AppUser> userManager)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
        _userManager = userManager;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        var comments = await _commentRepository.GetAllAsync();
        var commentDtos = comments.Select(c => c.ToCommentDto());
        return Ok(commentDtos);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var comments = await _commentRepository.GetByIdAsync(id);
        if (comments == null)
        {
            return NotFound();
        }
        return Ok(comments.ToCommentDto());
    }
    
    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> Create(int stockId, CreateCommentDto commentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (!await _stockRepository.StockExists(stockId))
            return BadRequest("Stock does not exist");

        var userName = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(userName);
        
        var commentModel = commentDto.ToCommentFromCreate(stockId);
        commentModel.AppUserId = appUser.Id;
        await _commentRepository.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCommentRequestDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var comment = await _commentRepository.UpdateAsync(id, updateDto.ToCommentFromUpdate());
        if (comment == null)
            return NotFound("Comment not found");
        return Ok(comment.ToCommentDto());
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var comment = await _commentRepository.DeleteAsync(id);
        if (comment == null)
            return NotFound("Comment does not exist");
        return Ok(comment);
    }
}