using AutoMapper;
using RabeenApi.Dtos;
using RabeenApi.Dtos.Achievement.Requests;
using RabeenApi.Dtos.Achievement.Results;
using RabeenApi.Repositories;
using RabeenApi.Validators.Achievement;

namespace RabeenApi.Services.Implementations;

public class AchievementService(IAchievementRepository achievementRepository, IMapper mapper)
{
    private readonly IAchievementRepository _achievementRepository = achievementRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResult<AchievementResult>> UpdateAchievementAsync(UpdateAchievementRequest request)
    {
        var result = new BaseResult<AchievementResult>();
        var validator = new UpdateAchievementRequestValidator();
        try
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                result.Code = Status.NotValid;
                result.ErrorMessage = validationResult.ToString("~");
            }
            else
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