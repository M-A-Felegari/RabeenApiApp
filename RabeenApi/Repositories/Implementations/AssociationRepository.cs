using DataAccess;
using DataAccess.Models;

namespace RabeenApi.Repositories.Implementations;

public class AssociationRepository(DataContext context) : GenericRepository<Association>(context),
    IAssociationRepository
{
    
}