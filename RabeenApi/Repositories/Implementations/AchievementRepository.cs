using DataAccess;
using DataAccess.Models;

namespace RabeenApi.Repositories.Implementations;

public class AchievementRepository(DataContext context) : GenericRepository<Achievement>(context),
    IAchievementRepository
{
}