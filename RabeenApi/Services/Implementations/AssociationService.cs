using AutoMapper;
using RabeenApi.Repositories;
using DataAccess.Models;
using RabeenApi.Dtos;
using RabeenApi.Dtos.Association.Requests;
using RabeenApi.Dtos.Association.Results;
using RabeenApi.Validators.Association;

namespace RabeenApi.Services.Implementations;

public class AssociationService(IAssociationRepository associationRepository, IMapper mapper, IFileSaver fileSaver)
{
    private readonly IAssociationRepository _associationRepository = associationRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IFileSaver _fileSaver = fileSaver;

    public async Task<BaseResult<List<AssociationInfoResult>>> GetAllAsync(GetAllAssociationsRequest request)
    {
        var result = new BaseResult<List<AssociationInfoResult>>();
        var validator = new GetAllAssociationsRequestValidator();
        try
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                result.Code = Status.NotValid;
                result.ErrorMessage = validationResult.ToString("~");
            }
            else
            {
                var associations =
                    await _associationRepository.GetLastsByPagination(request.PageNumber, request.PageLength);
                var associationResults = _mapper.Map<List<AssociationInfoResult>>(associations);
                result.Data = associationResults;
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

    public async Task<BaseResult<AssociationInfoResult>> GetAssociationInfoAsync(GetAssociationRequest request)
    {
        var result = new BaseResult<AssociationInfoResult>();
        try
        {
            var association = await _associationRepository.GetAsync(request.Id);
            if (association is null)
            {
                result.Code = Status.AssociationNotFound;
                result.ErrorMessage = $"Association with id {request.Id} not found";
            }
            else
            {
                var associationResult = _mapper.Map<AssociationInfoResult>(association);
                result.Data = associationResult;
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

    public async Task<BaseResult<AssociationInfoResult>> AddAssociationAsync(AddAssociationRequest request)
    {
        var result = new BaseResult<AssociationInfoResult>();
        var validator = new AddAssociationRequestValidator();
        try
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                result.Code = Status.NotValid;
                result.ErrorMessage = validationResult.ToString("~");
            }
            else
            {
                var association = _mapper.Map<Association>(request);
                await _associationRepository.AddAsync(association);
                var associationResult = _mapper.Map<AssociationInfoResult>(association);
                result.Data = associationResult;
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

    public async Task<BaseResult<AssociationInfoResult>> UpdateAssociationInfoAsync(UpdateAssociationRequest request)
    {
        var result = new BaseResult<AssociationInfoResult>();
        var validator = new UpdateAssociationRequestValidator();
        try
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                result.Code = Status.NotValid;
                result.ErrorMessage = validationResult.ToString("~");
            }
            else
            {
                var association = await _associationRepository.GetAsync(request.Id);
                if (association is null)
                {
                    result.Code = Status.AssociationNotFound;
                    result.ErrorMessage = $"Association with id {request.Id} not found";
                }
                else
                {
                    association = _mapper.Map<Association>(request);
                    await _associationRepository.UpdateAsync(association);
                    var associationResult = _mapper.Map<AssociationInfoResult>(association);
                    result.Data = associationResult;
                    result.Code = Status.Success;
                }
            }
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public async Task<BaseResult<object>> SetAssociationLogoAsync(SetAssociationLogoRequest request)
    {
        var result = new BaseResult<object>();
        var validator = new SetAssociationLogoRequestValidator();
        try
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                result.Code = Status.NotValid;
                result.ErrorMessage = validationResult.ToString("~");
            }
            else
            {
                var association = await _associationRepository.GetAsync(request.Id);
                if (association is null)
                {
                    result.Code = Status.AssociationNotFound;
                    result.ErrorMessage = $"Association with id {request.Id} not found";
                }
                else
                {
                    await _fileSaver.SaveFileAsync(request.Logo, $@"{FileSaver.SaveAssociationLogoPath}\{association.Id}.jpg");
                    result.Code = Status.Success;
                }
            }
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public async Task<BaseResult<object>> DeleteAssociationAsync(DeleteAssociationRequest request)
    {
        var result = new BaseResult<object>();
        try
        {
            var association = await _associationRepository.GetAsync(request.Id);
            if (association is null)
            {
                result.Code = Status.AssociationNotFound;
                result.ErrorMessage = $"Association with id {request.Id} not found";
            }
            else
            {
                //todo: first delete the cooperations
                await _associationRepository.DeleteAsync(association.Id);
                _fileSaver.RemoveFileIfExist($@"{FileSaver.SaveAssociationLogoPath}\{association.Id}.jpg");
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