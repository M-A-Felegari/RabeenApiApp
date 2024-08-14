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
}