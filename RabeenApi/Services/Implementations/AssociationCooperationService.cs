using AutoMapper;
using RabeenApi.Repositories;
using DataAccess.Models;
using RabeenApi.Dtos;
using RabeenApi.Dtos.AssociationCooperation.Requests;
using RabeenApi.Dtos.AssociationCooperation.Results;
using RabeenApi.Validators;
using RabeenApi.Validators.AssociationCooperation;

namespace RabeenApi.Services.Implementations
{
    public class AssociationCooperationService(
        IAssociationCooperationRepository cooperationRepository,
        IAssociationRepository associationRepository,
        IFileSaver fileSaver,
        IMapper mapper
    )
    {
        private readonly IAssociationCooperationRepository _cooperationRepository = cooperationRepository;
        private readonly IAssociationRepository _associationRepository = associationRepository;
        private readonly IFileSaver _fileSaver = fileSaver;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResult<PaginatedResult<AssociationCooperationResult>>> GetAllCooperationsAsync(
            int associationId, PaginationRequest request)
        {
            var result = new BaseResult<PaginatedResult<AssociationCooperationResult>>();
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
                    var totalPages = await _cooperationRepository.CountAsync() / request.PageLength + 1;
                    if (request.PageNumber > totalPages)
                    {
                        result.Code = Status.OutOfRangePage;
                        result.ErrorMessage = $"last page is {totalPages}";
                    }
                    else
                    {
                        var association = await _associationRepository.GetAsync(associationId);
                        if (association is null)
                        {
                            result.Code = Status.AssociationNotFound;
                            result.ErrorMessage = $"association with id {associationId} not found";
                        }
                        else
                        {
                            var cooperations = 
                                await _cooperationRepository.GetAllByAssociationIdAsync(associationId,
                                    request.PageNumber, request.PageLength);
                            var cooperationResults = _mapper.Map<List<AssociationCooperationResult>>(cooperations);
                        
                            result.Code = Status.Success;
                            result.Data = new PaginatedResult<AssociationCooperationResult>()
                            {
                                CurrentPage = request.PageNumber,
                                TotalPages = totalPages,
                                Items = cooperationResults
                            };
                        }
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

        public async Task<BaseResult<AssociationCooperationResult>> AddCooperationAsync(int associationId,
            AddCooperationRequest request)
        {
            var result = new BaseResult<AssociationCooperationResult>();
            var validator = new AddCooperationRequestValidator();
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
                    var association = await _associationRepository.GetAsync(associationId);
                    if (association is null)
                    {
                        result.Code = Status.AssociationNotFound;
                        result.ErrorMessage = $"association with id {associationId} not found";
                    }
                    else
                    {
                        var cooperation = _mapper.Map<AssociationCooperation>(request);
                        cooperation.AssociationId = associationId;
                        await _cooperationRepository.AddAsync(cooperation);
                        await _fileSaver
                            .SaveFileAsync(request.Image, $@"{FileSaver.SaveCooperationImagePath}\{cooperation.Id}.jpg");
                        var cooperationResult = _mapper.Map<AssociationCooperationResult>(cooperation);
                        result.Data = cooperationResult;
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

        public async Task<BaseResult<AssociationCooperationResult>> UpdateCooperationAsync( int id,
            UpdateCooperationRequest request)
        {
            var result = new BaseResult<AssociationCooperationResult>();
            var validator = new UpdateCooperationRequestValidator();
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
                    var existingCooperation = await _cooperationRepository.GetAsync(id);
                    if (existingCooperation is null)
                    {
                        result.Code = Status.CooperationNotFound;
                        result.ErrorMessage = $"Cooperation with id {id} not found";
                    }
                    else
                    {
                        var updatedCooperation = _mapper.Map<AssociationCooperation>(request);
                        updatedCooperation.AssociationId = existingCooperation.AssociationId; 
                        //if we don't do this it's association id will be 0 and thrown an axception
                        
                        await _cooperationRepository.UpdateAsync(updatedCooperation);
                        if (request.Image is not null)
                             await _fileSaver
                                .SaveFileAsync(request.Image, $@"{FileSaver.SaveCooperationImagePath}\{existingCooperation.Id}.jpg");

                        var cooperationResult = _mapper.Map<AssociationCooperationResult>(updatedCooperation);
                        result.Data = cooperationResult;
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

        public async Task<BaseResult<object>> DeleteCooperationAsync(int id)
        {
            var result = new BaseResult<object>();
            try
            {
                var cooperation = await _cooperationRepository.GetAsync(id);
                if (cooperation is null)
                {
                    result.Code = Status.CooperationNotFound;
                    result.ErrorMessage = $"Cooperation with id {id} not found";
                }
                else
                {
                    await _cooperationRepository.DeleteAsync(cooperation.Id);
                    _fileSaver.RemoveFileIfExist($@"{FileSaver.SaveCooperationImagePath}\{cooperation.Id}.jpg");
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
}