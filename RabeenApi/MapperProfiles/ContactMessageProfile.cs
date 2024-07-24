using AutoMapper;
using DataAccess.Models;
using RabeenApi.Dtos.ContactMessage.Requests;
using RabeenApi.Dtos.ContactMessage.Results;

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