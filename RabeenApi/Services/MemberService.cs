using DataAccess.Models;
using RabeenApi.Dtos.Requests;
using RabeenApi.Dtos.Results;
using RabeenApi.Repositories;

namespace RabeenApi.Services;

public class MemberService(IMemberRepository memberRepository)
{
    private readonly IMemberRepository _memberRepository = memberRepository;

    public async Task<BaseResult<List<MemberPreviewResult>>> GetAllMainMembersAsync()
    {
        var result = new BaseResult<List<MemberPreviewResult>>();
        try
        {
            var mainMembers = await _memberRepository.GetAllMainMembersAsync();

            List<MemberPreviewResult> membersPreview = mainMembers.Select(member =>
            {
                var memberPreview =
                    new MemberPreviewResult(member.Id, member.Name, member.Title, true);
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

    public async Task<BaseResult<MemberInfoResult>> AddNewMember(AddMemberRequest request)
    {
        var result = new BaseResult<MemberInfoResult>();
        try
        {
            var memberAchievements = request.Achievements.Select(achievement =>
            {
                return new Achievement()
                {
                    Title = achievement.Title,
                    Date = achievement.Date,
                    Description = achievement.Description
                };
            });

            var member = new Member()
            {
                Name = request.Name,
                Title = request.Title,
                About = request.About,
                IsMainMember = request.IsMain,
                Achievments = memberAchievements
            };

            var addedMember = await _memberRepository.AddAsync(member);

            var memberInfo = new MemberInfoResult(
                addedMember.Id,
                addedMember.Name,
                addedMember.Title,
                addedMember.About,
                addedMember.IsMainMember,
                addedMember.Achievments is not null ? addedMember.Achievments.ToList() : []
            );

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