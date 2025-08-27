using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dtos.Stock;
using WebApplication1.Mappers;

namespace WebApplication1.Services;



// Services/IStockService.cs
using System.Threading;
using System.Threading.Tasks;



public class StockServices
{
    private readonly ApplicationDBContext _context;

    public StockServices(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StockDto>> GetAllAsync()
    {
        var stocks = await _context.Stocks.ToListAsync();
        var stockDto = stocks.Select(s => s.ToStockDto());
        return stockDto;
    }

    public async Task<StockDto?> GetByIdAsync(int id)
    {
        var stockModel = await _context.Stocks.FindAsync(id);
        if (stockModel == null) return null;
        var stockDto = stockModel.ToStockDto();
        return stockDto;
    }

    public async Task<StockDto?> UpdateAsync(int id, UpdateStockRequestDto updateDto)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel == null)
        {
            return null;
        }
        stockModel.Symbol = updateDto.Symbol;
        stockModel.Company = updateDto.Company;
        stockModel.Purchase = updateDto.Purchase;
        stockModel.LastDiv = updateDto.LastDiv;
        stockModel.Industry = updateDto.Industry;
        stockModel.MarketCap = updateDto.MarketCap;
        
        await _context.SaveChangesAsync();
        return stockModel.ToStockDto();
    }

    public async Task<StockDto> CreateAsync(CreateStockRequestDto createDto)
    {
        var stockModel = createDto.ToStockFromCreateDto();
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        var stockDto = stockModel.ToStockDto();
        return stockDto;

    }

    public async Task<bool> DeleteAsync(int id)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel == null) return false;
        _context.Stocks.Remove(stockModel);
        await _context.SaveChangesAsync();
        return true;
    }
    
}