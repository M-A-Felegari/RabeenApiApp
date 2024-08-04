using RabeenApi.Dtos;
using RabeenApi.Strategies.ActionResultHandlerStrategy;

namespace RabeenApi.Factories;

public class ActionResultHandlersFactory
{
    private readonly Dictionary<Status, IActionResultHandler> _handlers = new();

    public ActionResultHandlersFactory()
    {
        _handlers.Add(Status.Success, new OkResultHandler());
        _handlers.Add(Status.NotValid, new BadRequestResultHandler());
        _handlers.Add(Status.MemberIsNotMain, new BadRequestResultHandler());
        _handlers.Add(Status.MemberNotFound, new NotFoundResultHandler());
        _handlers.Add(Status.AchievementNotFound, new NotFoundResultHandler());
        _handlers.Add(Status.ContactMessageNotFound, new NotFoundResultHandler());
        _handlers.Add(Status.AssociationNotFound, new NotFoundResultHandler());
        _handlers.Add(Status.CooperationNotFound, new NotFoundResultHandler());
        _handlers.Add(Status.UserAlreadyExist, new ConflictResultHandler());
        _handlers.Add(Status.UnAuthorizedUser, new UnAuthorizedResultHandler());
        _handlers.Add(Status.ExceptionThrown, new InternalErrorResultHandler());
        _handlers.Add(Status.OutOfRangePage, new NotFoundResultHandler());
    }

    public IActionResultHandler? Create(Status status)
        => _handlers.GetValueOrDefault(status);
}