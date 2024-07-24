using DataAccess;
using DataAccess.Models;

namespace RabeenApi.Repositories.Implementations;

public class AssociationCooperationRepository(DataContext context) : GenericRepository<AssociationCooperation>(context),
    IAssociationCooperationRepository
{
    
}