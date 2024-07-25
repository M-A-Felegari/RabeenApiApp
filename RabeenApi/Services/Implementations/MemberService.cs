using AutoMapper;
using DataAccess.Models;
using RabeenApi.Dtos;
using RabeenApi.Dtos.Achievement.Requests;
using RabeenApi.Dtos.Achievement.Results;
using RabeenApi.Dtos.Member.Requests;
using RabeenApi.Dtos.Member.Results;
using RabeenApi.Repositories;
using RabeenApi.Validators.Achievement;
using RabeenApi.Validators.Member;

namespace RabeenApi.Services.Implementations;

public class MemberService(IMemberRepository memberRepository, IMapper mapper,IFileSaver fileSaver)
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IFileSaver _fileSaver = fileSaver;

    public async Task<BaseResult<List<MemberPreviewResult>>> GetAllMainMembersAsync()
    {
        var result = new BaseResult<List<MemberPreviewResult>>();
        try
        {
            var mainMembers = await _memberRepository.GetAllMainMembersAsync();

            var membersPreview = _mapper.Map<List<MemberPreviewResult>>(mainMembers);

            result.Code = Status.Success;
            result.Data = membersPreview;
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public async Task<BaseResult<List<MemberPreviewResult>>> GetAllMembersAsync()
    {
        var result = new BaseResult<List<MemberPreviewResult>>();
        try
        {
            var allMembers = await _memberRepository.GetAllAsync();

            var membersPreview = _mapper.Map<List<MemberPreviewResult>>(allMembers);
            
            result.Code = Status.Success;
            result.Data = membersPreview;
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public async Task<BaseResult<MemberInfoResult>> GetMemberInformationAsync(GetMemberInformationRequest request)
    {
        var result = new BaseResult<MemberInfoResult>();
        try
        {
            var member = await _memberRepository.GetAsync(request.Id);

            if (member is null)
            {
                result.Code = Status.MemberNotFound;
                result.ErrorMessage = $"member with id {request.Id} not found";
            }
            else if (!member.IsMainMember)
            {
                result.Code = Status.MemberIsNotMain;
                result.ErrorMessage = $"member {member.Name} with id {member.Id} is not a main member";
            }
            else
            {
                var memberAchievements = await _memberRepository.GetMemberAchievementsAsync(member.Id);
                member.Achievements = memberAchievements;

                var memberInfo = _mapper.Map<MemberInfoResult>(member);

                result.Code = Status.Success;
                result.Data = memberInfo;
            }
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public async Task<BaseResult<MemberInfoResult>> AddNewMemberAsync(AddMemberRequest request)
    {
        var result = new BaseResult<MemberInfoResult>();
        var validator = new AddMemberRequestValidator();
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
                var member = _mapper.Map<Member>(request);

                var addedMember = await _memberRepository.AddAsync(member);
            
                var memberInfo = _mapper.Map<MemberInfoResult>(addedMember);

                result.Code = Status.Success;
                result.Data = memberInfo;
            }
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public async Task<BaseResult<object>> SetProfilePictureAsync(SetProfilePictureRequest request)
    {
        var result = new BaseResult<object>();
        var validator = new SetProfilePictureRequestValidator();
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


                var member = await _memberRepository.GetAsync(request.Id);

                if (member is null)
                {
                    result.Code = Status.MemberNotFound;
                    result.ErrorMessage = $"member with id {request.Id} not found";
                }
                else
                {
                    await _fileSaver.SaveFileAsync(request.Picture, $@"data\members-profile\{request.Id}.jpg");
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

    public async Task<BaseResult<object>> SetMemberCvAsync(SetMemberCvRequest request)
    {
        var result = new BaseResult<object>();
        var validator = new SetMemberCvRequestValidator();
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
                var member = await _memberRepository.GetAsync(request.Id);

                if (member is null)
                {
                    result.Code = Status.MemberNotFound;
                    result.ErrorMessage = $"member with id {request.Id} not found";
                }
                else
                {
                    await _fileSaver.SaveFileAsync(request.CvFile, $@"data\members-cv\{request.Id}.pdf");
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
    
    public async Task<BaseResult<MemberInfoResult>> UpdateMemberInfoAsync(UpdateMemberInfoRequest request)
    {
        var result = new BaseResult<MemberInfoResult>();
        var validator = new UpdateMemberInfoRequestValidator();
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
                var member = await _memberRepository.GetAsync(request.Id);

                if (member is null)
                {
                    result.Code = Status.MemberNotFound;
                    result.ErrorMessage = $"member with id {request.Id} not found";
                }
                else
                {
                    var updatedMember = _mapper.Map<Member>(request);
                    await _memberRepository.UpdateAsync(updatedMember);

                    var memberInfo = _mapper.Map<MemberInfoResult>(updatedMember);

                    result.Code = Status.Success;
                    result.Data = memberInfo;
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

    //object means we don't want to pass any data to this api response
    public async Task<BaseResult<object>> DeleteMemberAsync(DeleteMemberRequest request)
    {
        var result = new BaseResult<object>();
        try
        {
            var member = await _memberRepository.GetAsync(request.MemberId);
            if (member is null)
            {
                result.Code = Status.MemberNotFound;
                result.ErrorMessage = $"member with id {request.MemberId} not found";
            }
            else
            {
                await _memberRepository.DeleteAsync(member);

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

    public async Task<BaseResult<List<AchievementResult>>> AddAchievement(AddAchievementToExistMemberRequest request)
    {
        var result = new BaseResult<List<AchievementResult>>();
        var validator = new AddAchievementToExistMemberRequestValidator();
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
                var member = await _memberRepository.GetAsync(request.MemberId);

                if (member is null)
                {
                    result.Code = Status.MemberNotFound;
                    result.ErrorMessage = $"member with id {request.MemberId} not found";
                }
                else
                {
                    var achievement = _mapper.Map<Achievement>(request);
                    var updatedMember = await _memberRepository.AddAchievementToMemberAsync(member.Id, achievement);
                    var achievementResults = _mapper.Map<List<AchievementResult>>(updatedMember.Achievements);

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
}