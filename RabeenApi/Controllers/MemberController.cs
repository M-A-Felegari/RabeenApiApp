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
    ActionResultHandlersFactory handlersFactory) : ControllerBase
{
    private readonly MemberService _memberService  = memberService;
    private readonly ActionResultHandlersFactory _handlersFactory = handlersFactory;

    [HttpGet("all-main-members")]
    public async Task<ActionResult<BaseResult<List<MemberPreviewResult>>>> AllMainMembers()
    {
        var result = await _memberService.GetAllMainMembersAsync();

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
    
    [HttpPost("add-achievement-to-member")]
    public async Task<ActionResult<BaseResult<List<AchievementResult>>>> Add(
        AddAchievementToExistMemberRequest request)
    {
        var result = await _memberService.AddAchievement(request);

        return GetActionResultToReturn(result);
    }

    private ActionResult GetActionResultToReturn<T>(BaseResult<T> result)
    {
        var actionResult = _handlersFactory.GetHandler(result.Code);

        return actionResult is not null ? actionResult.Handle(result) : StatusCode(500, result);
    }
}