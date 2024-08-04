using RabeenApi.Dtos;
using RabeenApi.Dtos.Member.Requests;
using RabeenApi.Dtos.Member.Results;

namespace RabeenApi.Services;

public interface IMemberService
{
    Task<BaseResult<List<MemberPreviewResult>>> GetAllMainMembersAsync();
    Task<BaseResult<List<MemberPreviewResult>>> GetAllMembersAsync();
    Task<BaseResult<MemberInfoResult>> GetMemberInformationAsync(int id);
    Task<BaseResult<MemberInfoResult>> AddNewMemberAsync(AddMemberRequest request);
    Task<BaseResult<object>> SetProfilePictureAsync(int id,SetProfilePictureRequest request);
    Task<BaseResult<object>> SetMemberCvAsync(int id, SetMemberCvRequest request);
    Task<BaseResult<MemberInfoResult>> UpdateMemberInfoAsync(int id, UpdateMemberInfoRequest request);
    Task<BaseResult<object>> DeleteMemberAsync(int id);
}