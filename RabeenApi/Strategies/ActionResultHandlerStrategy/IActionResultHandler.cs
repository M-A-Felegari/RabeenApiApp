using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;

namespace RabeenApi.Strategies.ActionResultHandlerStrategy;

public interface IActionResultHandler
{
    public ActionResult Handle<T>(BaseResult<T> result);
}