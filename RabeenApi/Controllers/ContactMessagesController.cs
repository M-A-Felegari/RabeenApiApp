using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;
using RabeenApi.Dtos.ContactMessage.Requests;
using RabeenApi.Dtos.ContactMessage.Results;
using RabeenApi.Factories;
using RabeenApi.Services.Implementations;

namespace RabeenApi.Controllers;

[ApiController]
[Route("contact-messages")]
public class ContactMessagesController(ContactMessageService contactMessageService,
    ActionResultHandlersFactory handlersFactory) : BaseControllerClass(handlersFactory)
{
    private readonly ContactMessageService _contactMessageService = contactMessageService;
    
    [HttpGet()]
    public async Task<ActionResult<BaseResult<PaginatedResult<ContactMessageInfoResult>>>> GetAllMessagesAsync(
        PaginationRequest request)
    {
        var result = await _contactMessageService.GetAllMessagesAsync(request);
        
        return GetActionResultToReturn(result);
    }

    [HttpPost()]
    public async Task<ActionResult<BaseResult<ContactMessageInfoResult>>> AddMessageAsync(AddContactMessageRequest request)
    {
        var result = await _contactMessageService.AddMessageAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<BaseResult<object>>> DeleteMessageAsync(int id)
    {
        var result = await _contactMessageService.DeleteMessageAsync(id);

        return GetActionResultToReturn(result);
    }
}
