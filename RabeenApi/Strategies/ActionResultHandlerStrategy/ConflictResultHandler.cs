using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;

namespace RabeenApi.Strategies.ActionResultHandlerStrategy;

public class ConflictResultHandler : IActionResultHandler
{
    public ActionResult Handle<T>(BaseResult<T> result)
        => new ConflictObjectResult(result);
}