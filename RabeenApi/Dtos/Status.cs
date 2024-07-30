namespace RabeenApi.Dtos;

public enum Status
{
    ExceptionThrown = -1,
    Success = 0,
    NotValid,
    MemberNotFound = 10,
    MemberIsNotMain,
    AchievementNotFound = 20,
    ContactMessageNotFound = 30,
    AssociationNotFound = 40,
    CooperationNotFound = 50,
    OutOfRangePage = 100,
}