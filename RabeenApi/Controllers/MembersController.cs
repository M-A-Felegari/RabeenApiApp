using Microsoft.AspNetCore.Authorization;
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
[Route("members")]
public class MembersController(
    MemberService memberService,
    AchievementsService achievementsService,
    ActionResultHandlersFactory handlersFactory) : BaseControllerClass(handlersFactory)
{
    private readonly MemberService _memberService = memberService;
    private readonly AchievementsService _achievementsService = achievementsService;

    [HttpGet("all-main-members")]
    public async Task<ActionResult<BaseResult<List<MemberPreviewResult>>>> AllMainMembersAsync()
    {
        var result = await _memberService.GetAllMainMembersAsync();

        return GetActionResultToReturn(result);
    }

    [HttpGet("")]
    public async Task<ActionResult<BaseResult<List<MemberPreviewResult>>>> GetAllMembersAsync()
    {
        var result = await _memberService.GetAllMembersAsync();

        return GetActionResultToReturn(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BaseResult<MemberInfoResult>>> GetMemberInfoAsync(int id)
    {
        var result = await _memberService.GetMemberInformationAsync(id);

        return GetActionResultToReturn(result);
    }

    [HttpGet("{id:int}/achievements")]
    public async Task<ActionResult<BaseResult<List<AchievementResult>>>> GetAchievements(int id)
    {
        var result = await _achievementsService.GetAll(id);

        return GetActionResultToReturn(result);
    }

    [HttpPost("{id:int}/achievements")]
    [Authorize("ManagerPolicy")]
    public async Task<ActionResult<BaseResult<List<AchievementResult>>>> AddAchievementToMemberAsync(
        int id, AddAchievementRequest request)
    {
        var result = await _achievementsService.AddAchievement(id, request);

        return GetActionResultToReturn(result);
    }

    [HttpPost()]
    [Authorize("ManagerPolicy")]
    public async Task<ActionResult<BaseResult<MemberInfoResult>>> Add(AddMemberRequest request)
    {
        var result = await _memberService.AddNewMemberAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpPost("{id:int}/set-profile")]
    [Authorize("ManagerPolicy")]
    public async Task<ActionResult<BaseResult<object>>> SetProfileAsync(int id,
        [FromForm] SetProfilePictureRequest request)
    {
        var result = await _memberService.SetProfilePictureAsync(id, request);

        return GetActionResultToReturn(result);
    }

    [HttpPost("{id:int}/set-cv")]
    [Authorize("ManagerPolicy")]
    public async Task<ActionResult<BaseResult<object>>> SetCvAsync(int id, [FromForm] SetMemberCvRequest request)
    {
        var result = await _memberService.SetMemberCvAsync(id, request);

        return GetActionResultToReturn(result);
    }

    [HttpPut("{id:int}")]
    [Authorize("ManagerPolicy")]
    public async Task<ActionResult<BaseResult<MemberInfoResult>>> UpdateMemberAsync(int id,
        UpdateMemberInfoRequest request)
    {
        var result = await _memberService.UpdateMemberInfoAsync(id, request);

        return GetActionResultToReturn(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize("ManagerPolicy")]
    public async Task<ActionResult<BaseResult<object>>> AllMainMembers(int id)
    {
        var result = await _memberService.DeleteMemberAsync(id);

        return GetActionResultToReturn(result);
    }
}