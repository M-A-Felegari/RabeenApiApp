using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;

namespace RabeenApi.Strategies.ActionResultHandlerStrategy;

public class BadRequestResultHandler : IActionResultHandler
{
    public ActionResult Handle<T>(BaseResult<T> result)
    {
        return new BadRequestObjectResult(result);
    }
}