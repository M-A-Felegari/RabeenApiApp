using AutoMapper;
using RabeenApi.Repositories;
using DataAccess.Models;
using RabeenApi.Dtos;
using RabeenApi.Dtos.Association.Requests;
using RabeenApi.Dtos.Association.Results;
using RabeenApi.Helpers;
using RabeenApi.Validators;
using RabeenApi.Validators.Association;

namespace RabeenApi.Services.Implementations;

public class AssociationService(IAssociationRepository associationRepository, IMapper mapper, IFileSaver fileSaver)
    : IAssociationService
{
    private readonly IAssociationRepository _associationRepository = associationRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IFileSaver _fileSaver = fileSaver;

    public async Task<BaseResult<PaginatedResult<AssociationInfoResult>>> GetAllAsync(PaginationRequest request)
    {
        var result = new BaseResult<PaginatedResult<AssociationInfoResult>>();
        var validator = new PaginationRequestValidator();
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
                var totalAssociations = await _associationRepository.CountAsync();
                var totalPages = PaginationHelper.CalculateTotalPages(totalAssociations, request.PageLength);
                if (request.PageNumber > totalPages)
                {
                    result.Code = Status.OutOfRangePage;
                    result.ErrorMessage = $"last page is {totalPages}";
                }
                else
                {
                    var associations =
                        await _associationRepository.GetSortedByTotalCooperations(request.PageNumber,
                            request.PageLength);
                    var associationResults = _mapper.Map<List<AssociationInfoResult>>(associations);

                    for (var i = 0; i < associationResults.Count; i++)
                    {
                        var totalCooperationsCount =
                            await _associationRepository.CountTotalCooperationsAsync(associationResults[i].Id);

                        var firstCooperationDate =
                            await _associationRepository.GetFirstCooperationDateAsync(associationResults[i].Id);
                        
                        associationResults[i] = associationResults[i] with
                        {
                            TotalCooperations = totalCooperationsCount,
                            FirstCooperationDate = firstCooperationDate
                        };
                    }

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
                var totalCooperationsNumber = await _associationRepository.CountTotalCooperationsAsync(id);
                var firstCooperationDate = await _associationRepository.GetFirstCooperationDateAsync(id);
                associationResult = associationResult with
                {
                    TotalCooperations = totalCooperationsNumber,
                    FirstCooperationDate = firstCooperationDate
                };

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

    public async Task<BaseResult<AssociationInfoResult>> UpdateAssociationInfoAsync(int id,
        UpdateAssociationRequest request)
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
                    var updatedAssociation = _mapper.Map<Association>(request);
                    updatedAssociation.Id = association.Id;
                    await _associationRepository.UpdateAsync(updatedAssociation);

                    var associationResult = _mapper.Map<AssociationInfoResult>(updatedAssociation);

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
                    await _fileSaver.SaveFileAsync(request.Logo,
                        $@"{FileSaver.SaveAssociationLogoPath}\{association.Id}.jpg");
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