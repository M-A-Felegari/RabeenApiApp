using RabeenApi.Dtos.Results;
using RabeenApi.Strategies.ActionResultHandlerStrategy;

namespace RabeenApi.Factories;

public class ActionResultHandlersFactory
{
    private readonly Dictionary<Status,IActionResultHandler> _handlers = new();
    public ActionResultHandlersFactory()
    {
        _handlers.Add(Status.Success,new OkResultHandler());
        _handlers.Add(Status.ExceptionThrown, new InternalErrorResultHandler());
        _handlers.Add(Status.MemberNotFound, new NotFoundResultHandler());
        _handlers.Add(Status.AchievementNotFound, new NotFoundResultHandler());
        _handlers.Add(Status.ContactMessageNotFound, new NotFoundResultHandler());
    }

    public IActionResultHandler? GetHandler(Status status)
        => _handlers.GetValueOrDefault(status);
}