using DataAccess;
using DataAccess.Models;

namespace RabeenApi.Repositories.Implementaions;

public class AchievementRepository(DataContext context) : GenericRepository<Achievement>(context),
    IAchievementRepository
{
}