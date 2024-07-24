namespace RabeenApi.Dtos.Results;

public enum Status
{
    ExceptionThrown = -1,
    Success = 0,
    MemberNotFound = 10,
    MemberIsNotMain,
    AchievementNotFound = 20,
    ContactMessageNotFound = 30,
    AssociationNotFound=40,
}
