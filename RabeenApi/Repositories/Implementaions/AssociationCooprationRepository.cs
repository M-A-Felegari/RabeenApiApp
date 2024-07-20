using DataAccess;
using DataAccess.Models;

namespace RabeenApi.Repositories.Implementaions;

public class AssociationCooprationRepository(DataContext context) : GenericRepository<AssociationCoopration>(context),
    IAssociationCooprationRepository
{
    
}