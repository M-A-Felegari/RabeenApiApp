using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace RabeenApi.Repositories.Implementations;

public class UserRepository(DataContext context) : GenericRepository<User>(context),IUserRepository
{
    public async Task<bool> IsAlreadyUsedUsernameAsync(string username)
    {
        var existUser = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username);
        return existUser is not null;
    }

    public async Task<User?> GetByUsernameAndPasswordAsync(string username, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        return user;
    }

}