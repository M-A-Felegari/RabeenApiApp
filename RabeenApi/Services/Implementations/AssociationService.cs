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

    public async Task<BaseResult<PaginatedResult<AssociationInfoResult>>> GetAllAsync(GetAllAssociationsRequest request)
    {
        var result = new BaseResult<PaginatedResult<AssociationInfoResult>>();
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
                var totalPages = await _associationRepository.CountAsync() / request.PageLength;
                if (request.PageNumber > totalPages)
                {
                    result.Code = Status.OutOfRangePage;
                    result.ErrorMessage = $"last page is {totalPages}";
                }
                else
                {
                    var associations =
                        await _associationRepository.GetLastsByPagination(request.PageNumber, request.PageLength);
                    var associationResults = _mapper.Map<List<AssociationInfoResult>>(associations);
                    
                    result.Code = Status.Success;
                    result.Data = new PaginatedResult<AssociationInfoResult>()
                    {
                        CurrentPage = request.PageNumber,
                        TotalPages = totalPages,
                        Items = associationResults
                    };
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

    public async Task<BaseResult<AssociationInfoResult>> UpdateAssociationInfoAsync(int id,UpdateAssociationRequest request)
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
                var association = await _associationRepository.GetAsync(id);
                if (association is null)
                {
                    result.Code = Status.AssociationNotFound;
                    result.ErrorMessage = $"Association with id {id} not found";
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

    public async Task<BaseResult<object>> SetAssociationLogoAsync(int id, SetAssociationLogoRequest request)
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
                var association = await _associationRepository.GetAsync(id);
                if (association is null)
                {
                    result.Code = Status.AssociationNotFound;
                    result.ErrorMessage = $"Association with id {id} not found";
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