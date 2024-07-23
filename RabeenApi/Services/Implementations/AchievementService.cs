using AutoMapper;
using DataAccess.Models;
using RabeenApi.Dtos.Requests;
using RabeenApi.Dtos.Results;
using RabeenApi.Repositories;

namespace RabeenApi.Services.Implementations;

public class AchievementService(IAchievementRepository achievementRepository, IMapper mapper)
{
    private readonly IAchievementRepository _achievementRepository = achievementRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResult<AchievementResult>> UpdateAchievementAsync(UpdateAchievementRequest request)
    {
        var result = new BaseResult<AchievementResult>();
        try
        {
            var achievement = await _achievementRepository.GetAsync(request.Id);
            if (achievement is null)
            {
                result.Code = Status.AchievementNotFound;
                result.ErrorMessage = $"Achievement with id {request.Id} didn't found";
            }
            else
            {
                var achievementResult = _mapper.Map<AchievementResult>(achievement);
                result.Data = achievementResult;
                result.Code = Status.Success;
            }
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public async Task<BaseResult<object>> DeleteAchievementAsync(DeleteAchievementRequest request)
    {
        var result = new BaseResult<object>();
        try
        {
            var achievement = await _achievementRepository.GetAsync(request.Id);
            if (achievement is null)
            {
                result.Code = Status.AchievementNotFound;
                result.ErrorMessage = $"Achievement with id {request.Id} didn't found";
            }
            else
            {
                await _achievementRepository.DeleteAsync(request.Id);
                result.Code = Status.Success;
            }
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }
}