using AutoMapper;
using DataAccess.Models;
using RabeenApi.Dtos.Member.Requests;
using RabeenApi.Dtos.Member.Results;

namespace RabeenApi.MapperProfiles;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<AddMemberRequest, Member>();
        
        CreateMap<UpdateMemberInfoRequest, Member>();
        
        CreateMap<Member, MemberInfoResult>()
            .ConstructUsing((src) => new MemberInfoResult(
                src.Id,
                src.Name,
                src.Title,
                src.About,
                src.IsMainMember,
                src.OwnPortfolio
            ));

        CreateMap<Member, MemberPreviewResult>()
            .ConstructUsing(src => new MemberPreviewResult(
                src.Id,
                src.Name,
                src.Title,
                src.IsMainMember,
                src.OwnPortfolio
            ));

    }
}