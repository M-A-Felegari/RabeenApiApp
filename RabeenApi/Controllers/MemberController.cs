using DataAccess.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos.Results;
using RabeenApi.Repositories;
using RabeenApi.Services;
namespace RabeenApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class MemberController(MemberService memberService) : ControllerBase
{
    private readonly MemberService _memberService  = memberService;

    [HttpGet("all-primary-members")]
    public async Task<ActionResult<BaseResult<List<MemberPreviewResult>>>>
        AllPrimaryMembers()
    {
        var result = await _memberService.GetAllPrimaryMembersAsync();

        if (result.Code == Status.ExceptionThrown)
            return StatusCode(500, result);

        return Ok(result);
    }
}