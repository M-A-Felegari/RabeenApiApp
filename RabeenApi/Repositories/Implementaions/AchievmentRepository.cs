using DataAccess;
using DataAccess.Models;

namespace RabeenApi.Repositories.Implementaions;

public class AchievmentRepository(DataContext context) : GenericRepository<Achievment>(context),
    IAchievmentRepository
{
    
}