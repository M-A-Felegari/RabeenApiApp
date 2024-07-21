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
        
        member.IsMainMember = isPrimary;
        await _context.SaveChangesAsync();

        return member;
    }

    public async Task<List<Member>> GetAllMainMembersAsync()
    {
        var members = await _context.Members.Where(m=>m.IsMainMember).ToListAsync();
        return members;
    }

    public async Task<List<Achievement>> GetMemberAchievementsAsync(int memberId)
    {
        var member = await _context.Members.Include(m => m.Achievments)
            .FirstOrDefaultAsync(m => m.Id == memberId);
        if (member is null)
            throw new KeyNotFoundException($"member with id {memberId} not found");
        return member.Achievments.ToList();
    }
}