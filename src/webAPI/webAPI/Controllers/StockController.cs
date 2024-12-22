using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAPI.Data;
using webAPI.Dtos.Stock;
using webAPI.Helpers;
using webAPI.Interfaces;
using webAPI.Mappers;

namespace webAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockRepository _stockRepository;
    private readonly ApplicationDbContext _context;
    public StockController(ApplicationDbContext context, IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var stock = await _stockRepository.GetAllAsync(query);
        var stockDto = stock.Select(x => x.ToStockDto()).ToList();
        return Ok(stockDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var stock = await _stockRepository.GetByIdAsync(id);
        if (stock == null)
            return NotFound();
        return Ok(stock.ToStockDto());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateStockRequestDto stockDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var stock = stockDto.ToStockFromCreateDto();
        await _stockRepository.CreateAsync(stock);
        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
    }
    
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateStockRequestDto stockDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var stock = await _stockRepository.UpdateAsync(id, stockDto);
        if (stock == null)
            return NotFound();
        return Ok(stock.ToStockDto());
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var stock = await _stockRepository.DeleteAsync(id);
        if (stock == null)
            return NotFound();
        return NoContent();
    }
}