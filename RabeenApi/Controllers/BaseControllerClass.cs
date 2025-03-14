﻿using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;
using RabeenApi.Factories;

namespace RabeenApi.Controllers;

public abstract class BaseControllerClass(ActionResultHandlersFactory handlersFactory) : ControllerBase
{
        private readonly ActionResultHandlersFactory _handlersFactory = handlersFactory;

        protected ActionResult GetActionResultToReturn<T>(BaseResult<T> result)
        {
            var actionResult = _handlersFactory.Create(result.Code);

            return actionResult is not null ? actionResult.Handle(result) : StatusCode(500, result);
        }

}