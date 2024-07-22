using AutoMapper;
using DataAccess.Models;
using RabeenApi.Dtos.Requests;
using RabeenApi.Dtos.Results;
using RabeenApi.Repositories;

namespace RabeenApi.Services;

public class MemberService(IMemberRepository memberRepository, IMapper mapper)
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    private readonly IMapper _mapper = mapper;

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

            List<MemberPreviewResult> membersPreview = allMembers.Select(member =>
            {
                var memberPreview =
                    new MemberPreviewResult(member.Id, member.Name, member.Title, member.IsMainMember);
                return memberPreview;
            }).ToList();

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

    public async Task<BaseResult<MemberInfoResult>> GetMemberInformation(int memberId)
    {
        var result = new BaseResult<MemberInfoResult>();
        try
        {
            var member = await _memberRepository.GetAsync(memberId);

            if (member is null)
                result.Code = Status.MemberNotFound;
            else if (!member.IsMainMember)
                result.Code = Status.MemberIsNotMain;
            else
            {
                var memberAchievements = await _memberRepository.GetMemberAchievementsAsync(memberId);
                var memberInfo = new MemberInfoResult(
                    member.Id,
                    member.Name,
                    member.Title,
                    member.About,
                    member.IsMainMember,
                    memberAchievements
                );

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
        try
        {
            var member = _mapper.Map<Member>(request);

            var addedMember = await _memberRepository.AddAsync(member);

            var memberInfo = _mapper.Map<MemberInfoResult>(addedMember);

            result.Code = Status.Success;
            result.Data = memberInfo;
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }
}