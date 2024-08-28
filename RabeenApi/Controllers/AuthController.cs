using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;
using RabeenApi.Dtos.User.Requests;
using RabeenApi.Dtos.User.Results;
using RabeenApi.Factories;
using RabeenApi.Services;
using RabeenApi.Services.Implementations;

namespace RabeenApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(ActionResultHandlersFactory handlersFactory,IAuthService authService)
    :BaseControllerClass(handlersFactory)
{
    private readonly IAuthService _authService = authService;
    [HttpPost("signup")]
    [Authorize("ManagerPolicy")]
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