using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using DataAccess.Models;
using Microsoft.IdentityModel.Tokens;
using RabeenApi.Dtos;
using RabeenApi.Dtos.User.Requests;
using RabeenApi.Dtos.User.Results;
using RabeenApi.Repositories;
using RabeenApi.Validators.User;

namespace RabeenApi.Services.Implementations;

public class AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IConfiguration _configuration = configuration;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResult<UserTokenResult>> SignUpAsync(UserSignupRequest request)
    {
        var result = new BaseResult<UserTokenResult>();
        var validator = new UserSignupRequestValidator();
        try
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                result.Code = Status.NotValid;
                result.ErrorMessage = validationResult.ToString("~");
            }
            else
            {
                var isAlreadyUsed = await _userRepository.IsAlreadyUsedUsernameAsync(request.Username);
                if (isAlreadyUsed)
                {
                    result.Code = Status.UserAlreadyExist;
                    result.ErrorMessage = $"username {request.Username} already exist";
                }
                else
                {
                    var user = _mapper.Map<User>(request);
                    await _userRepository.AddAsync(user);
                    var token = GenerateJwtToken(user);

                    result.Code = Status.Success;
                    result.Data = new UserTokenResult(token);
                }
            }
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public async Task<BaseResult<UserTokenResult>> LoginAsync(UserLoginRequest request)
    {
        var result = new BaseResult<UserTokenResult>();
        var validator = new UserLoginRequestValidator();
        try
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                result.Code = Status.NotValid;
                result.ErrorMessage = validationResult.ToString("~");
            }
            else
            {
                var user = await _userRepository.GetByUsernameAndPasswordAsync(request.Username, request.Password);
                if (user is null)
                {
                    result.Code = Status.UnAuthorizedUser;
                    result.ErrorMessage = $"username or password is incorrect";
                }
                else
                {
                    var token = GenerateJwtToken(user);

                    result.Code = Status.Success;
                    result.Data = new UserTokenResult(token);
                }
            }
        }
        catch (Exception ex)
        {
            result.Code = Status.ExceptionThrown;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(jwtSettings["Key"] ??
                      throw new NullReferenceException("Jwt must not be null")));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            },
            expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"] ?? "60")),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}