using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace RabeenApi.Repositories.Implementations;

public class AssociationRepository(DataContext context) : GenericRepository<Association>(context),
    IAssociationRepository
{
    public async Task<List<Association>> GetSortedByTotalCooperations(int page, int length)
    {
        var associations = await _context.Associations
            .Include(a=>a.Cooprations)
            .OrderByDescending(a=>a.Cooprations.Count())
            .Skip((page - 1) * length)
            .Take(length)
            .ToListAsync();

        return associations;
    }

    public async Task<int> CountTotalCooperationsAsync(int associationId)
    {
        var association = await _context
            .Associations
            .FindAsync(associationId);

        if (association is null)
            throw new NullReferenceException($"association with id {association} is null");

        var cooperationsNumber = await _context
            .AssociationCooperations
            .CountAsync(c => c.AssociationId == associationId);

        return cooperationsNumber;
    }

    public async Task<DateTime> GetFirstCooperationDateAsync(int associationId)
    {
        var association = await _context
            .Associations
            .FindAsync(associationId);

        if (association is null)
            throw new NullReferenceException($"association with id {association} is null");

        var firstCooperation = await _context
            .AssociationCooperations
            .OrderByDescending(c=>c.StartDate)
            .FirstOrDefaultAsync(c => c.AssociationId == associationId);

        return firstCooperation?.StartDate ?? new DateTime();
    }
}