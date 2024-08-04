namespace RabeenApi.Dtos;

public enum Status
{
    //here [added] means "this enum item added to ActionResultHandler factory and handled its own action result to create"
    
    ExceptionThrown = -1, //added
    Success = 0, //added
    NotValid = 1, //added
    MemberNotFound = 10, //added
    MemberIsNotMain = 11, //added
    AchievementNotFound = 20, //added
    ContactMessageNotFound = 30, //added
    AssociationNotFound = 40, //added
    CooperationNotFound = 50, //added
    UserAlreadyExist = 61, //added
    UnAuthorizedUser = 62, //added
    OutOfRangePage = 100, //added
}