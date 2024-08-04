using DataAccess.Models;

namespace RabeenApi.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<bool> IsAlreadyUsedUsernameAsync(string username);
    public Task<User?> GetByUsernameAndPasswordAsync(string username, string password);
}