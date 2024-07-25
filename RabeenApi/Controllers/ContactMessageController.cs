using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;
using RabeenApi.Dtos.ContactMessage.Requests;
using RabeenApi.Dtos.ContactMessage.Results;
using RabeenApi.Factories;
using RabeenApi.Services.Implementations;

namespace RabeenApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class ContactMessageController(ContactMessageService contactMessageService,
    ActionResultHandlersFactory handlersFactory) : BaseControllerClass(handlersFactory)
{
    private readonly ContactMessageService _contactMessageService = contactMessageService;

    [HttpGet("all-messages")]
    public async Task<ActionResult<BaseResult<List<ContactMessageInfoResult>>>> GetAllMessagesAsync(
        [FromQuery] GetAllContactMessagesRequest request)
    {
        var result = await _contactMessageService.GetAllMessagesAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpPost("add-message")]
    public async Task<ActionResult<BaseResult<ContactMessageInfoResult>>> AddMessageAsync(AddContactMessageRequest request)
    {
        var result = await _contactMessageService.AddMessageAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpDelete("delete-message")]
    public async Task<ActionResult<BaseResult<object>>> DeleteMessageAsync([FromQuery] DeleteContactMessageRequest request)
    {
        var result = await _contactMessageService.DeleteMessageAsync(request);

        return GetActionResultToReturn(result);
    }
}
