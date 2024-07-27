using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace RabeenApi.Repositories.Implementations;

public class AssociationCooperationRepository(DataContext context) : GenericRepository<AssociationCooperation>(context),
    IAssociationCooperationRepository
{
    public async Task<List<AssociationCooperation>> GetAllByAssociationIdAsync(int associationId, int pageNumber,
        int pageLength)
    {
        var association = await _context.Associations
            .Include(a => a.Cooprations)
            .FirstOrDefaultAsync(a => a.Id == associationId);
        if (association is null)
            throw new KeyNotFoundException($"association with id {associationId} not found");

        var cooperations = association.Cooprations
            .Skip(pageLength * (pageNumber - 1))
            .Take(pageLength)
            .ToList();

        return cooperations;
    }
}