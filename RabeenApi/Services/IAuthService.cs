using RabeenApi.Dtos;
using RabeenApi.Dtos.User.Requests;
using RabeenApi.Dtos.User.Results;

namespace RabeenApi.Services;

public interface IAuthService
{
    Task<BaseResult<UserTokenResult>> SignUpAsync(UserSignupRequest request);
    Task<BaseResult<UserTokenResult>> LoginAsync(UserLoginRequest request);
}