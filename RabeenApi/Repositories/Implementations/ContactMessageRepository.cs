using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace RabeenApi.Repositories.Implementations;

public class ContactMessageRepository(DataContext context) : GenericRepository<ContactMessage>(context),
    IContactMessageRepository
{
}