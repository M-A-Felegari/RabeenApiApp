using AutoMapper;
using DataAccess.Models;
using RabeenApi.Dtos.Requests;
using RabeenApi.Dtos.Results;

namespace RabeenApi.MapperProfiles;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<AddMemberRequest, Member>();
        
        CreateMap<Member, MemberInfoResult>()
            .ConstructUsing(src => new MemberInfoResult(
                src.Id,
                src.Name,
                src.Title,
                src.About,
                src.IsMainMember,
                src.Achievements != null ? src.Achievements.ToList() : new List<Achievement>()
            ));

        CreateMap<Member, MemberPreviewResult>()
            .ConstructUsing(src => new MemberPreviewResult(
                src.Id,
                src.Name,
                src.Title,
                src.IsMainMember
            ));
    }
}