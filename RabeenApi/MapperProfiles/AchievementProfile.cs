using AutoMapper;
using DataAccess.Models;
using RabeenApi.Dtos.Requests;
using RabeenApi.Dtos.Results;

namespace RabeenApi.MapperProfiles;

public class AchievementProfile : Profile
{
    public AchievementProfile()
    {
        CreateMap<AddAchievementRequest, Achievement>();
        
        CreateMap<AddAchievementToExistMemberRequest, Achievement>();
            
        
        CreateMap<Achievement, AchievementResult>()
            .ConstructUsing(src => new AchievementResult(
                src.Id,
                src.Title,
                src.Description,
                src.Date
            ));
    }
}