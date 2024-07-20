using DataAccess;
using DataAccess.Models;

namespace RabeenApi.Repositories.Implementaions;

public class ContactRequestRepository(DataContext context) : GenericRepository<ContactRequest>(context),
    IContactRequestRepository
{
    
}