using AutoMapper;
using DataAccess.Models;
using RabeenApi.Dtos.Requests;
using RabeenApi.Dtos.Results;

namespace RabeenApi.MapperProfiles;

public class ContactMessageProfile : Profile
{
    public ContactMessageProfile()
    {
        CreateMap<AddContactMessageRequest, ContactMessage>();
        
        CreateMap<ContactMessage, ContactMessageInfoResult>()
            .ConstructUsing(src => new ContactMessageInfoResult(
                src.Id,
                src.Name,
                src.Email,
                src.Subject,
                src.Text
            ));
    }
}