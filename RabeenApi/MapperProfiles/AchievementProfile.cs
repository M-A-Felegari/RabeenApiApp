using AutoMapper;
using DataAccess.Models;
using RabeenApi.Dtos.Requests;

namespace RabeenApi.MapperProfiles;

public class AchievementProfile : Profile
{
    public AchievementProfile()
    {
        CreateMap<AddAchievementRequest, Achievement>();
    }
}