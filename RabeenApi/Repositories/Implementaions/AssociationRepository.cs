using DataAccess;
using DataAccess.Models;

namespace RabeenApi.Repositories.Implementaions;

public class AssociationRepository(DataContext context) : GenericRepository<Association>(context),
    IAssociationRepository
{
    
}