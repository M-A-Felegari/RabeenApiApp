using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;
using RabeenApi.Dtos.Achievement.Requests;
using RabeenApi.Dtos.Achievement.Results;
using RabeenApi.Dtos.Member.Requests;
using RabeenApi.Dtos.Member.Results;
using RabeenApi.Factories;
using RabeenApi.Services.Implementations;
namespace RabeenApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class MemberController(MemberService memberService,
    ActionResultHandlersFactory handlersFactory) : BaseControllerClass(handlersFactory)
{
    private readonly MemberService _memberService  = memberService;

    [HttpGet("all-main-members")]
    public async Task<ActionResult<BaseResult<List<MemberPreviewResult>>>> AllMainMembersAsync()
    {
        var result = await _memberService.GetAllMainMembersAsync();

        return GetActionResultToReturn(result);
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<BaseResult<List<MemberPreviewResult>>>> GetAllMembersAsync()
    {
        var result = await _memberService.GetAllMembersAsync();

        return GetActionResultToReturn(result);
    }

    [HttpGet("info")]
    public async Task<ActionResult<BaseResult<MemberInfoResult>>> GetMemberInfoAsync(GetMemberInformationRequest request)
    {
        var result = await _memberService.GetMemberInformationAsync(request);

        return GetActionResultToReturn(result);
    }
    
    [HttpPost("add")]
    public async Task<ActionResult<BaseResult<MemberInfoResult>>> Add(AddMemberRequest request)
    {
        var result = await _memberService.AddNewMemberAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpPost("set-profile")]
    public async Task<ActionResult<BaseResult<object>>> SetProfileAsync([FromForm] SetProfilePictureRequest request)
    {
        var result = await _memberService.SetProfilePictureAsync(request);

        return GetActionResultToReturn(result);
    }
    
    [HttpPost("set-cv")]
    public async Task<ActionResult<BaseResult<object>>> SetCvAsync([FromForm] SetMemberCvRequest request)
    {
        var result = await _memberService.SetMemberCvAsync(request);

        return GetActionResultToReturn(result);
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<BaseResult<MemberInfoResult>>> AllMainMembers(UpdateMemberInfoRequest request)
    {
        var result = await _memberService.UpdateMemberInfoAsync(request);

        return GetActionResultToReturn(result);
    }
    [HttpPost("add-achievement-to-member")]
    public async Task<ActionResult<BaseResult<List<AchievementResult>>>> Add(
        AddAchievementToExistMemberRequest request)
    {
        var result = await _memberService.AddAchievement(request);

        return GetActionResultToReturn(result);
    }
    
    [HttpDelete("delete")]
    public async Task<ActionResult<BaseResult<object>>> AllMainMembers(DeleteMemberRequest request)
    {
        var result = await _memberService.DeleteMemberAsync(request);

        return GetActionResultToReturn(result);
    }
}