using DataAccess.Models;

namespace RabeenApi.Repositories;

public interface IAchievementRepository : IGenericRepository<Achievement>
{
    public Task<List<Achievement>> GetMemberAchievementsAsync(int memberId);
    public Task<List<Achievement>> AddAchievementToMemberAsync(int memberId, Achievement achievement);
}