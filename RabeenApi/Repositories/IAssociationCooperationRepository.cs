using DataAccess.Models;

namespace RabeenApi.Repositories;

public interface IAssociationCooperationRepository : IGenericRepository<AssociationCooperation>
{
    public Task<List<AssociationCooperation>> GetAllByAssociationIdAsync(int associationId, int pageNumber,
        int pageLength);
}