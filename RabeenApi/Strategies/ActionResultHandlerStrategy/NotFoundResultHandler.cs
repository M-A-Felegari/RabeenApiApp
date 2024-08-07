﻿using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;

namespace RabeenApi.Strategies.ActionResultHandlerStrategy;

public class NotFoundResultHandler : IActionResultHandler
{
    public ActionResult Handle<T>(BaseResult<T> result)
        => new NotFoundObjectResult(result);
}