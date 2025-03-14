﻿using AutoMapper;
using DataAccess.Models;
using RabeenApi.Dtos;
using RabeenApi.Dtos.ContactMessage.Requests;
using RabeenApi.Dtos.ContactMessage.Results;
using RabeenApi.Helpers;
using RabeenApi.Repositories;
using RabeenApi.Validators;
using RabeenApi.Validators.ContactMessage;

namespace RabeenApi.Services.Implementations;

public class ContactMessageService(IContactMessageRepository contactMessageRepository, IMapper mapper)
    : IContactMessageService
{
    private readonly IContactMessageRepository _contactMessageRepository = contactMessageRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResult<PaginatedResult<ContactMessageInfoResult>>> GetAllMessagesAsync(
        PaginationRequest request)
    {
        var result = new BaseResult<PaginatedResult<ContactMessageInfoResult>>();
        var validator = new PaginationRequestValidator();
        try
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                result.Code = Status.NotValid;
                result.ErrorMessage = validationResult.ToString("~");
                return result;
            }

            var totalMessages = await _contactMessageRepository.CountAsync();
            var totalPages = PaginationHelper.CalculateTotalPages(totalMessages, request.PageLength);
            if (request.PageNumber > totalPages)
            {
                result.Code = Status.OutOfRangePage;
                result.ErrorMessage = $"last page is {totalPages}";
                return result;
            }

            var messages = await _contactMessageRepository
                .GetLastsByPagination(request.PageNumber, request.PageLength);

            var messageInfoResults = _mapper.Map<List<ContactMessageInfoResult>>(messages);
            result.Code = Status.Success;
            result.Data = new PaginatedResult<ContactMessageInfoResult>
            {
                Items = messageInfoResults,
                CurrentPage = request.PageNumber,
                TotalPages = totalPages
            };
            return result;
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
            return result;
        }
    }

    public async Task<BaseResult<ContactMessageInfoResult>> AddMessageAsync(
        AddContactMessageRequest request)
    {
        var result = new BaseResult<ContactMessageInfoResult>();
        var validator = new AddContactMessageRequestValidator();
        try
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                result.Code = Status.NotValid;
                result.ErrorMessage = validationResult.ToString("~");
                return result;
            }

            var message = _mapper.Map<ContactMessage>(request);

            var addedMessage = await _contactMessageRepository.AddAsync(message);

            var addedMessageResult = _mapper.Map<ContactMessageInfoResult>(addedMessage);

            result.Code = Status.Success;
            result.Data = addedMessageResult;
            return result;
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
            return result;
        }
    }

    public async Task<BaseResult<object>> DeleteMessageAsync(int id)
    {
        var result = new BaseResult<object>();
        try
        {
            var message = await _contactMessageRepository.GetAsync(id);

            if (message is null)
            {
                result.Code = Status.ContactMessageNotFound;
                result.ErrorMessage = $"message with id {id} not found";
                return result;
            }

            await _contactMessageRepository.DeleteAsync(message.Id);
            result.Code = Status.Success;
            return result;
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
            return result;
        }
    }
}