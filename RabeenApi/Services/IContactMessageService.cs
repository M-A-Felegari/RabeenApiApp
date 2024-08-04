using RabeenApi.Dtos;
using RabeenApi.Dtos.ContactMessage.Requests;
using RabeenApi.Dtos.ContactMessage.Results;

namespace RabeenApi.Services;

public interface IContactMessageService
{
    Task<BaseResult<PaginatedResult<ContactMessageInfoResult>>> GetAllMessagesAsync(
        PaginationRequest request);

    Task<BaseResult<ContactMessageInfoResult>> AddMessageAsync(
        AddContactMessageRequest request);

    Task<BaseResult<object>> DeleteMessageAsync(int id);
}