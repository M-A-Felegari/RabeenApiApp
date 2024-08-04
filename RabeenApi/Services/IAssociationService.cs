using RabeenApi.Dtos;
using RabeenApi.Dtos.Association.Requests;
using RabeenApi.Dtos.Association.Results;

namespace RabeenApi.Services;

public interface IAssociationService
{
    Task<BaseResult<PaginatedResult<AssociationInfoResult>>> GetAllAsync(PaginationRequest request);
    Task<BaseResult<AssociationInfoResult>> GetAssociationInfoAsync(int id);
    Task<BaseResult<AssociationInfoResult>> AddAssociationAsync(AddAssociationRequest request);
    Task<BaseResult<AssociationInfoResult>> UpdateAssociationInfoAsync(int id,UpdateAssociationRequest request);
    Task<BaseResult<object>> SetAssociationLogoAsync(int id, SetAssociationLogoRequest request);
    Task<BaseResult<object>> DeleteAssociationAsync(int id);
}