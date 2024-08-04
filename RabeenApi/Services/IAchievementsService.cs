using RabeenApi.Dtos;
using RabeenApi.Dtos.Achievement.Requests;
using RabeenApi.Dtos.Achievement.Results;

namespace RabeenApi.Services;

public interface IAchievementsService
{
    Task<BaseResult<List<AchievementResult>>> GetAll(int memberId);
    Task<BaseResult<List<AchievementResult>>> AddAchievement(int memberId,AddAchievementRequest request);
    Task<BaseResult<AchievementResult>> UpdateAchievementAsync(int id,UpdateAchievementRequest request);
    Task<BaseResult<object>> DeleteAchievementAsync(int id);
}