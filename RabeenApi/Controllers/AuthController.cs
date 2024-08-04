using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;
using RabeenApi.Dtos.ContactMessage.Results;
using RabeenApi.Dtos.User.Requests;
using RabeenApi.Dtos.User.Results;
using RabeenApi.Factories;
using RabeenApi.Services.Implementations;

namespace RabeenApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(ActionResultHandlersFactory handlersFactory,AuthService authService)
    :BaseControllerClass(handlersFactory)
{
    private readonly AuthService _authService = authService;
    [HttpPost("signup")]
    public async Task<ActionResult<BaseResult<UserTokenResult>>> SignupAsync(UserSignupRequest request)
    {
        var result = await _authService.SignUpAsync(request);
        
        return GetActionResultToReturn(result);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<BaseResult<UserTokenResult>>> LoginAsync(UserLoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        
        return GetActionResultToReturn(result);
    }
}