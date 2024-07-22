using DataAccess.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos.Requests;
using RabeenApi.Dtos.Results;
using RabeenApi.Repositories;
using RabeenApi.Services;
namespace RabeenApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class MemberController(MemberService memberService) : ControllerBase
{
    private readonly MemberService _memberService  = memberService;

    [HttpGet("all-main-members")]
    public async Task<ActionResult<BaseResult<List<MemberPreviewResult>>>> AllMainMembers()
    {
        var result = await _memberService.GetAllMainMembersAsync();

        return result.Code == Status.ExceptionThrown ? StatusCode(500, result) : Ok(result);
    }

    [HttpPost("add")]
    public async Task<ActionResult<BaseResult<MemberInfoResult>>> Add(AddMemberRequest request)
    {
        var result = await _memberService.AddNewMemberAsync(request);

        // return swtich (result.Code)
        // {
        //     Status.Success => Ok(result);
        // }

        return result;
    }
}