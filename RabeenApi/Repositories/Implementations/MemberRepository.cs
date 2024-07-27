using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace RabeenApi.Repositories.Implementations;

public class MemberRepository(DataContext context) : GenericRepository<Member>(context),
    IMemberRepository
{

    public async Task<List<Member>> GetAllMainMembersAsync()
    {
        var members = await _context.Members.Where(m => m.IsMainMember).ToListAsync();
        return members;
    }
}