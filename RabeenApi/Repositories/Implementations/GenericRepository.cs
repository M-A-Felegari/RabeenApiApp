using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace RabeenApi.Repositories.Implementations;

public class GenericRepository<T>(DataContext context) : IGenericRepository<T>
    where T : BaseModel
{
    protected readonly DataContext _context = context;

    public async Task<List<T>> GetAllAsync()
    {
        var result = await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
        return result;
    }
    
    public async Task<List<T>> GetLastsByPagination(int pageNumber, int pageLength)
    {
        var messages = await _context.Set<T>()
            .AsNoTracking()
            .OrderByDescending(c => c.Id)
            .Skip((pageNumber - 1) * pageLength)
            .Take(pageLength)
            .ToListAsync();

        return messages;

    }

    public async Task<T?> GetAsync(int id)
    {
        var result = await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
        
        return result;
    }

    public async Task<int> CountAsync()
    {
        var totalItemsCount = await _context.Set<T>()
            .CountAsync();
        
        return totalItemsCount;
    }

    public async Task<T> AddAsync(T model)
    {
        await _context.Set<T>().AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task UpdateAsync(T model)
    {
        _context.Set<T>().Update(model);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T model)
    {
        _context.Set<T>().Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var model = await GetAsync(id);
        if (model is not null)
        {
            await DeleteAsync(model);
        }
        else
        {
            throw new KeyNotFoundException($"{typeof(T)} with key {id} not found");
        }
    }

}