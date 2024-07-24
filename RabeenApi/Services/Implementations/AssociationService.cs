using AutoMapper;
using RabeenApi.Dtos.Requests;
using RabeenApi.Dtos.Results;
using RabeenApi.Repositories;
using DataAccess.Models;

namespace RabeenApi.Services.Implementations;

public class AssociationService(IAssociationRepository associationRepository, IMapper mapper, IFileSaver fileSaver)
{
    private readonly IAssociationRepository _associationRepository = associationRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IFileSaver _fileSaver = fileSaver;

    public async Task<BaseResult<List<AssociationInfoResult>>> GetAllAsync(int pageNumber, int pageLength)
    {
        var result = new BaseResult<List<AssociationInfoResult>>();
        try
        {
            var associations = await _associationRepository.GetLastsByPagination(pageNumber, pageLength);
            var associationResults = _mapper.Map<List<AssociationInfoResult>>(associations);
            result.Data = associationResults;
            result.Code = Status.Success;
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public async Task<BaseResult<AssociationInfoResult>> GetAssociationInfoAsync(int id)
    {
        var result = new BaseResult<AssociationInfoResult>();
        try
        {
            var association = await _associationRepository.GetAsync(id);
            if (association is null)
            {
                result.Code = Status.AssociationNotFound;
                result.ErrorMessage = $"Association with id {id} not found";
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
        try
        {
            var association = _mapper.Map<Association>(request);
            await _associationRepository.AddAsync(association);
            var associationResult = _mapper.Map<AssociationInfoResult>(association);
            result.Data = associationResult;
            result.Code = Status.Success;
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
                association = _mapper.Map<Association>(request);
                await _associationRepository.UpdateAsync(association);
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

    public async Task<BaseResult<object>> SetAssociationLogoAsync(SetAssociationLogoRequest request)
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
                await _fileSaver.SaveFileAsync(request.Logo, $@"data\association-logos\{request.Id}.jpg");
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

    public async Task<BaseResult<object>> DeleteAssociationAsync(int id)
    {
        var result = new BaseResult<object>();
        try
        {
            var association = await _associationRepository.GetAsync(id);
            if (association is null)
            {
                result.Code = Status.AssociationNotFound;
                result.ErrorMessage = $"Association with id {id} not found";
            }
            else
            {
                //todo: first delete the cooperations
                await _associationRepository.DeleteAsync(id);
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