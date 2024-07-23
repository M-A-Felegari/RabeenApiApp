using DataAccess.Models;
using RabeenApi.Dtos.Requests;
using RabeenApi.Dtos.Results;
using RabeenApi.Repositories;

namespace RabeenApi.Services.Implementations;

public class AchievementService(IAchievementRepository achievementRepository,
     IMemberRepository memberRepository)
{
     private readonly IAchievementRepository _achievementRepository = achievementRepository;
     private readonly IMemberRepository _memberRepository = memberRepository;
     
}