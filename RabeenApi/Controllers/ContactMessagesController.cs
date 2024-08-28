using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;
using RabeenApi.Dtos.ContactMessage.Requests;
using RabeenApi.Dtos.ContactMessage.Results;
using RabeenApi.Factories;
using RabeenApi.Services;
using RabeenApi.Services.Implementations;

namespace RabeenApi.Controllers;

[ApiController]
[Route("contact-messages")]
public class ContactMessagesController(IContactMessageService contactMessageService,
    ActionResultHandlersFactory handlersFactory) : BaseControllerClass(handlersFactory)
{
    private readonly IContactMessageService _contactMessageService = contactMessageService;
    
    [HttpGet]
    [Authorize("ManagerOrAdminPolicy")]
    public async Task<ActionResult<BaseResult<PaginatedResult<ContactMessageInfoResult>>>> GetAllMessagesAsync(
        PaginationRequest request)
    {
        var result = await _contactMessageService.GetAllMessagesAsync(request);
        
        return GetActionResultToReturn(result);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResult<ContactMessageInfoResult>>> AddMessageAsync(AddContactMessageRequest request)
    {
        var result = await _contactMessageService.AddMessageAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize("ManagerOrAdminPolicy")]
    public async Task<ActionResult<BaseResult<object>>> DeleteMessageAsync(int id)
    {
        var result = await _contactMessageService.DeleteMessageAsync(id);

        return GetActionResultToReturn(result);
    }
}
