using AutoMapper;
using RabeenApi.Repositories;
using DataAccess.Models;
using RabeenApi.Dtos;
using RabeenApi.Dtos.AssociationCooperation.Requests;
using RabeenApi.Dtos.AssociationCooperation.Results;
using RabeenApi.Validators.AssociationCooperation;

namespace RabeenApi.Services.Implementations
{
    public class AssociationCooperationServiceAssociationCooperationService(
        IAssociationCooperationRepository cooperationRepository,
        IFileSaver fileSaver,
        IMapper mapper
    )
    {
        private readonly IAssociationCooperationRepository _cooperationRepository = cooperationRepository;
        private readonly IFileSaver _fileSaver = fileSaver;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResult<List<AssociationCooperationResult>>> GetAllCooperationsAsync(
            GetAllAssociationCooperationsRequest request)
        {
            var result = new BaseResult<List<AssociationCooperationResult>>();
            var validator = new GetAllAssociationCooperationsRequestValidator();
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
                    var cooperations =
                        await _cooperationRepository.GetAllByAssociationIdAsync(request.AssociationId,
                            request.PageNumber, request.PageLength);
                    var cooperationResults = _mapper.Map<List<AssociationCooperationResult>>(cooperations);
                    result.Data = cooperationResults;
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

        public async Task<BaseResult<AssociationCooperationResult>> AddCooperationAsync(AddCooperationRequest request)
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
                    var cooperation = _mapper.Map<AssociationCooperation>(request);
                    await _cooperationRepository.AddAsync(cooperation);
                    await _fileSaver.SaveFileAsync(request.Image, $@"data\cooperation-images\{cooperation.Id}.jpg");
                    var cooperationResult = _mapper.Map<AssociationCooperationResult>(cooperation);
                    result.Data = cooperationResult;
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

        public async Task<BaseResult<AssociationCooperationResult>> UpdateCooperationAsync(
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
                    var cooperation = await _cooperationRepository.GetAsync(request.Id);
                    if (cooperation is null)
                    {
                        result.Code = Status.CooperationNotFound;
                        result.ErrorMessage = $"Cooperation with id {request.Id} not found";
                    }
                    else
                    {
                        _mapper.Map(request, cooperation);
                        await _cooperationRepository.UpdateAsync(cooperation);
                        await _fileSaver.SaveFileAsync(request.Image, $@"data\cooperation-images\{cooperation.Id}.jpg");

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

        public async Task<BaseResult<object>> DeleteCooperationAsync(DeleteCooperationRequest request)
        {
            var result = new BaseResult<object>();
            try
            {
                var cooperation = await _cooperationRepository.GetAsync(request.Id);
                if (cooperation is null)
                {
                    result.Code = Status.CooperationNotFound;
                    result.ErrorMessage = $"Cooperation with id {request.Id} not found";
                }
                else
                {
                    await _cooperationRepository.DeleteAsync(request.Id);
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