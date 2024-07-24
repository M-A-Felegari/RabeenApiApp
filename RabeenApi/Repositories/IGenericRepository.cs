using DataAccess;
using DataAccess.Models;

namespace RabeenApi.Repositories;

public interface IGenericRepository<T> 
{
    public Task<List<T>> GetAllAsync();
    public Task<List<T>> GetLastsByPagination(int pageNumber, int pageLength);
    public Task<T?> GetAsync(int id);
    public Task<T> AddAsync(T model);
    public Task UpdateAsync(T model);
    public Task DeleteAsync(T model);
    public Task DeleteAsync(int id);
}