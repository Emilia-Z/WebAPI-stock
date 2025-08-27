using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dtos.Stock;
using WebApplication1.Mappers;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController: ControllerBase
{
    // private readonly ApplicationDBContext _context;
    private StockServices _stockService;

    public StockController(StockServices stockService)
    {
        _stockService = stockService;
    }
    
    // public StockController(ApplicationDBContext context)
    // {
    //     _context = context;
    // }

    // [HttpGet]
    // public async Task<IActionResult> GetAll()
    // {
    //     var stocks = await _context.Stocks.ToListAsync();
    //     var stockDto = stocks.Select(s => s.ToStockDto());
    //     return Ok(stockDto);
    // }

    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetById([FromRoute] int id)
    // {
    //     var stock = await _context.Stocks.FindAsync(id);
    //     if (stock == null)
    //     {
    //         return NotFound();
    //     }
    //     return Ok(stock.ToStockDto());
    // }    
    
    // [HttpPost]
    // public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    // {
    //     var stockModel = stockDto.ToStockFromCreateDto();
    //     await _context.Stocks.AddAsync(stockModel);
    //     await _context.SaveChangesAsync();
    //     return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto() );
    // }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto createDto)
    {
        var stockDto = await _stockService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new {id = stockDto.Id}, stockDto);
    }

    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var stockDto = await _stockService.GetAllAsync();
        return Ok(stockDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stockDto = await _stockService.GetByIdAsync(id);
        return stockDto is null ? NotFound() : Ok(stockDto);
    }
    
    
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        var stockDto = await _stockService.UpdateAsync(id, updateDto);
        return stockDto is null ? NotFound() : Ok(stockDto);
    }
    
    

    // [HttpDelete]
    // [Route("{id}")]
    // public async Task<IActionResult> Delete([FromRoute] int id)
    // {
    //     var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
    //     if (stockModel == null)
    //     {
    //         return NotFound();
    //     }
    //     _context.Stocks.Remove(stockModel);
    //     await _context.SaveChangesAsync();
    //     return NoContent();
    // }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAll([FromRoute] int id)
    {
        var result = await _stockService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
    
}

