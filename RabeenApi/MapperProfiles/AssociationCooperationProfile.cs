using AutoMapper;
using DataAccess.Models;
using RabeenApi.Dtos.AssociationCooperation.Requests;
using RabeenApi.Dtos.AssociationCooperation.Results;

namespace RabeenApi.MapperProfiles;

public class AssociationCooperationProfile:Profile
{
    public AssociationCooperationProfile()
    {
        CreateMap<AddCooperationRequest, AssociationCooperation>();

        CreateMap<UpdateCooperationRequest, AssociationCooperation>();
        
        CreateMap<AssociationCooperation, AssociationCooperationResult>()
            .ConstructUsing(src => new AssociationCooperationResult(
                src.Id,
                src.Title,
                src.Description,
                src.StartDate,
                src.FinishDate
            ));
    }
}