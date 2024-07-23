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
        var member = await _context.Members.Include(m => m.Achievements)
            .FirstOrDefaultAsync(m => m.Id == memberId);
        if (member is null)
            throw new KeyNotFoundException($"member with id {memberId} not found");
        return member.Achievements.ToList();
    }

    public async Task<Member> AddAchievementToMemberAsync(int memberId, Achievement achievement)
    {
        var member = await _context.Members.Include(m => m.Achievements)
            .FirstOrDefaultAsync(m => m.Id == memberId);
        if (member is null)
            throw new KeyNotFoundException($"member with id {memberId} not found");

        var achievementsList = member.Achievements is not null ? member.Achievements.ToList() : [];
        
        achievementsList.Add(achievement);

        member.Achievements = achievementsList;
        
        _context.Update(member);
        await _context.SaveChangesAsync();

        return member;
    }
}