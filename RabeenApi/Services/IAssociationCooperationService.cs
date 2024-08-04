using RabeenApi.Dtos;
using RabeenApi.Dtos.AssociationCooperation.Requests;
using RabeenApi.Dtos.AssociationCooperation.Results;

namespace RabeenApi.Services;

public interface IAssociationCooperationService
{
    Task<BaseResult<PaginatedResult<AssociationCooperationResult>>> GetAllCooperationsAsync(
        int associationId, PaginationRequest request);

    Task<BaseResult<AssociationCooperationResult>> AddCooperationAsync(int associationId,
        AddCooperationRequest request);

    Task<BaseResult<AssociationCooperationResult>> UpdateCooperationAsync( int id,
        UpdateCooperationRequest request);

    Task<BaseResult<object>> DeleteCooperationAsync(int id);
}