using AutoMapper;
using DataAccess.Models;
using RabeenApi.Dtos.User.Requests;

namespace RabeenApi.MapperProfiles;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<UserSignupRequest, User>();
    }
}