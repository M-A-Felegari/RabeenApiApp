using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos.Results;

namespace RabeenApi.Strategies.ActionResultHandlerStrategy;

public class InternalErrorResultHandler : IActionResultHandler
{
    public ActionResult Handle<T>(BaseResult<T> result)
    {
        return new ObjectResult(result)
        {
            StatusCode = 500
        };
    }
}