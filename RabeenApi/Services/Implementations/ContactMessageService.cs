using AutoMapper;
using DataAccess.Models;
using RabeenApi.Dtos;
using RabeenApi.Dtos.ContactMessage.Requests;
using RabeenApi.Dtos.ContactMessage.Results;
using RabeenApi.Repositories;

namespace RabeenApi.Services.Implementations;

public class ContactMessageService(IContactMessageRepository contactMessageRepository, IMapper mapper)
{
    private readonly IContactMessageRepository _contactMessageRepository = contactMessageRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResult<List<ContactMessageInfoResult>>> GetAllMessagesAsync(
        GetAllContactMessagesRequest request)
    {
        var result = new BaseResult<List<ContactMessageInfoResult>>();
        try
        {
            var messages = await _contactMessageRepository
                .GetLastsByPagination(request.PageNumber, 20);

            var messageInfoResults = _mapper.Map<List<ContactMessageInfoResult>>(messages);
            result.Code = Status.Success;
            result.Data = messageInfoResults;

        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }
    
    public async Task<BaseResult<ContactMessageInfoResult>> AddMessageAsync(
        AddContactMessageRequest request)
    {
        var result = new BaseResult<ContactMessageInfoResult>();
        try
        {
            var message = _mapper.Map<ContactMessage>(request);

            var addedMessage = await _contactMessageRepository.AddAsync(message);
            
            var addedMessageResult = _mapper.Map<ContactMessageInfoResult>(addedMessage);
            
            result.Code = Status.Success;
            result.Data = addedMessageResult;
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public async Task<BaseResult<object>> DeleteMessageAsync(DeleteContactMessageRequest request)
    {
        var result = new BaseResult<object>();
        try
        {
            var message = await _contactMessageRepository.GetAsync(request.Id);

            if (message is null)
            {
                result.Code = Status.ContactMessageNotFound;
                result.ErrorMessage = $"message with id {request.Id} not found";
            }
            else
            {
                await _contactMessageRepository.DeleteAsync(request.Id);
                result.Code = Status.Success;
            }
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }
}