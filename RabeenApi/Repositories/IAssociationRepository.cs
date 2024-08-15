using DataAccess.Models;

namespace RabeenApi.Repositories;

public interface IAssociationRepository : IGenericRepository<Association>
{
    public Task<List<Association>> GetSortedByTotalCooperations(int page, int length);
    public Task<int> CountTotalCooperationsAsync(int associationId);
}