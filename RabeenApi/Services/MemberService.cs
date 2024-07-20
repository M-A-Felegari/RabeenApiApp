using RabeenApi.Dtos.Results;
using RabeenApi.Repositories;

namespace RabeenApi.Services;

public class MemberService(IMemberRepository memberRepository)
{
    private readonly IMemberRepository _memberRepository = memberRepository;

    public async Task<BaseResult<List<MemberPreviewResult>>> GetAllPrimaryMembersAsync()
    {
        var result = new BaseResult<List<MemberPreviewResult>>();
        try
        {
            var data = new List<MemberPreviewResult>();

            var members = await _memberRepository.GetAllPrimaryAsync();

            data = members.Select(member =>
            {
                var memberPreview =
                    new MemberPreviewResult(member.Id, member.Name, member.Title);
                return memberPreview;
            }).ToList();

            result.Code = Status.Success;
            result.Data = data;
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }
        return result;

    }
}
