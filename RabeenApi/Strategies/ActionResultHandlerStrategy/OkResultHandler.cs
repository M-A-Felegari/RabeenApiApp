using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;

namespace RabeenApi.Strategies.ActionResultHandlerStrategy;

public class OkResultHandler : IActionResultHandler
{
    public ActionResult Handle<T>(BaseResult<T> result)
        => new OkObjectResult(result);
}