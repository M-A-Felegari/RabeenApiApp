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

public class MemberService(IMemberRepository memberRepository, IMapper mapper,IFileSaver fileSaver) : IMemberService
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

    //changed
    public async Task<BaseResult<MemberInfoResult>> GetMemberInformationAsync(int id)
    {
        var result = new BaseResult<MemberInfoResult>();
        try
        {
            var member = await _memberRepository.GetAsync(id);

            if (member is null)
            {
                result.Code = Status.MemberNotFound;
                result.ErrorMessage = $"member with id {id} not found";
            }
            else if (!member.IsMainMember)
            {
                result.Code = Status.MemberIsNotMain;
                result.ErrorMessage = $"member {member.Name} with id {member.Id} is not a main member";
            }
            else
            {

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

    //changed
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

    public async Task<BaseResult<object>> SetProfilePictureAsync(int id,SetProfilePictureRequest request)
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


                var member = await _memberRepository.GetAsync(id);

                if (member is null)
                {
                    result.Code = Status.MemberNotFound;
                    result.ErrorMessage = $"member with id {id} not found";
                }
                else
                {
                    await _fileSaver.SaveFileAsync(request.Picture, $@"{FileSaver.SaveProfilePath}\{member.Id}.jpg");
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

    public async Task<BaseResult<object>> SetMemberCvAsync(int id, SetMemberCvRequest request)
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
                var member = await _memberRepository.GetAsync(id);

                if (member is null)
                {
                    result.Code = Status.MemberNotFound;
                    result.ErrorMessage = $"member with id {id} not found";
                }
                else
                {
                    await _fileSaver.SaveFileAsync(request.CvFile, $@"{FileSaver.SaveCvPath}\{member.Id}.pdf");
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
    
    public async Task<BaseResult<MemberInfoResult>> UpdateMemberInfoAsync(int id, UpdateMemberInfoRequest request)
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
                var member = await _memberRepository.GetAsync(id);

                if (member is null)
                {
                    result.Code = Status.MemberNotFound;
                    result.ErrorMessage = $"member with id {id} not found";
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
    public async Task<BaseResult<object>> DeleteMemberAsync(int id)
    {
        var result = new BaseResult<object>();
        try
        {
            var member = await _memberRepository.GetAsync(id);
            if (member is null)
            {
                result.Code = Status.MemberNotFound;
                result.ErrorMessage = $"member with id {id} not found";
            }
            else
            {
                await _memberRepository.DeleteAsync(member);
                _fileSaver.RemoveFileIfExist($@"{FileSaver.SaveProfilePath}\{member.Id}.jpg");
                _fileSaver.RemoveFileIfExist($@"{FileSaver.SaveCvPath}\{member.Id}.pdf");

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