using AutoMapper;
using DataAccess.Models;
using RabeenApi.Dtos;
using RabeenApi.Dtos.Achievement.Requests;
using RabeenApi.Dtos.Achievement.Results;
using RabeenApi.Repositories;
using RabeenApi.Validators.Achievement;

namespace RabeenApi.Services.Implementations;

public class AchievementsService(
    IAchievementRepository achievementRepository,
    IMapper mapper,
    IMemberRepository memberRepository) : IAchievementsService
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    private readonly IAchievementRepository _achievementRepository = achievementRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResult<List<AchievementResult>>> GetAll(int memberId)
    {
        var result = new BaseResult<List<AchievementResult>>();
        try
        {
            var member = await _memberRepository.GetAsync(memberId);

            if (member is null)
            {
                result.Code = Status.MemberNotFound;
                result.ErrorMessage = $"member with id {memberId} not found";
            }
            else
            {
                var achievements = await _achievementRepository.GetMemberAchievementsAsync(member.Id);
                var achievementResults = _mapper.Map<List<AchievementResult>>(achievements);

                result.Code = Status.Success;
                result.Data = achievementResults;
            }
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public async Task<BaseResult<List<AchievementResult>>> AddAchievement(int memberId,AddAchievementRequest request)
    {
        var result = new BaseResult<List<AchievementResult>>();
        var validator = new AddAchievementRequestValidator();
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
                var member = await _memberRepository.GetAsync(memberId);

                if (member is null)
                {
                    result.Code = Status.MemberNotFound;
                    result.ErrorMessage = $"member with id {memberId} not found";
                }
                else
                {
                    var achievement = _mapper.Map<Achievement>(request);
                    var achievements = await _achievementRepository.AddAchievementToMemberAsync(member.Id, achievement);
                    var achievementResults = _mapper.Map<List<AchievementResult>>(achievements);

                    result.Code = Status.Success;
                    result.Data = achievementResults;
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

    public async Task<BaseResult<AchievementResult>> UpdateAchievementAsync(int id,UpdateAchievementRequest request)
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
                var achievement = await _achievementRepository.GetAsync(id);
                if (achievement is null)
                {
                    result.Code = Status.AchievementNotFound;
                    result.ErrorMessage = $"Achievement with id {id} didn't found";
                }
                else
                {
                    var updatedAchievement = _mapper.Map<Achievement>(request);
                    updatedAchievement.Id = achievement.Id;
                    await _achievementRepository.UpdateAsync(updatedAchievement);
                    
                    var achievementResult = _mapper.Map<AchievementResult>(updatedAchievement);
                    
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

    public async Task<BaseResult<object>> DeleteAchievementAsync(int id)
    {
        var result = new BaseResult<object>();
        try
        {
            var achievement = await _achievementRepository.GetAsync(id);
            if (achievement is null)
            {
                result.Code = Status.AchievementNotFound;
                result.ErrorMessage = $"Achievement with id {id} didn't found";
            }
            else
            {
                await _achievementRepository.DeleteAsync(achievement.Id);
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