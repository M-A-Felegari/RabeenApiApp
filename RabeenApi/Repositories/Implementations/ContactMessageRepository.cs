using DataAccess;
using DataAccess.Models;

namespace RabeenApi.Repositories.Implementations;

public class ContactMessageRepository(DataContext context) : GenericRepository<ContactMessage>(context),
    IContactMessageRepository
{
}