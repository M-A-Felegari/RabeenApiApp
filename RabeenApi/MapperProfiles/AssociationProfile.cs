using AutoMapper;
using DataAccess.Models;
using RabeenApi.Dtos.Association.Requests;
using RabeenApi.Dtos.Association.Results;

namespace RabeenApi.MapperProfiles;

public class AssociationProfile : Profile
{
    public AssociationProfile()
    {
        CreateMap<AddAssociationRequest, Association>();

        CreateMap<UpdateAssociationRequest, Association>();
        
        CreateMap<Association, AssociationInfoResult>()
            .ConstructUsing(src => new AssociationInfoResult(
                src.Id,
                src.Name,
                src.UniversityName,
                src.ContactLink,
                src.CreationDate,
                new DateTime(),
                0
            ));
    }
}