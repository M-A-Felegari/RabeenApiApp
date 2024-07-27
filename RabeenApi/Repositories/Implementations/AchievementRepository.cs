using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace RabeenApi.Repositories.Implementations;

public class AchievementRepository(DataContext context) : GenericRepository<Achievement>(context),
    IAchievementRepository
{
    public async Task<List<Achievement>> GetMemberAchievementsAsync(int memberId)
    {
        var member = await _context.Members
            .AsNoTracking()
            .Include(m => m.Achievements)
            .FirstOrDefaultAsync(m => m.Id == memberId);
        if (member is null)
            throw new KeyNotFoundException($"member with id {memberId} not found");
        //todo: test if there isn't any achievement don't throw exception
        return member.Achievements is not null ? member.Achievements.ToList() : [];
    }

    public async Task<List<Achievement>> AddAchievementToMemberAsync(int memberId, Achievement achievement)
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

        return member.Achievements.ToList();
    }
}