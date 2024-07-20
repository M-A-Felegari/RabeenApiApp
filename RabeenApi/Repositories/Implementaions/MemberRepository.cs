using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace RabeenApi.Repositories.Implementaions;

public class MemberRepository(DataContext context) : GenericRepository<Member>(context),
    IMemberRepository
{
    public async Task<Member> ChangeMemberPrimaryAsync(int id, bool isPrimary)
    {
        var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
        if (member is null)
            throw new KeyNotFoundException("this member is not existing");
        
        member.IsPrimaryMember = isPrimary;
        await _context.SaveChangesAsync();

        return member;
    }
}