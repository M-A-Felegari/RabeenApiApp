using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using RabeenApi.Repositories;

namespace RabeenApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserController(IMemberRepository memberRepository)
{
    private readonly IMemberRepository _memberRepository = memberRepository;

    //[HttpGet]
    //public async Task<Member> GetAsync(string id)
    //{
    //    _memberRepository.
        
    //}
}