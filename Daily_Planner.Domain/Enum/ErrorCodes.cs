namespace Daily_Planner.Domain.Enum;

public enum ErrorCodes
{
    ReportsNotFound = 0,
    ReportNotFound = 1,
    ReportAlreadyExist = 2,
    
    InternalServerError = 10,
    
    UserNotFound = 11,
    UserAlreadyExists = 12,
    UserUnauthorizedAccess = 13,
    UserAlreadyHadThisRole = 14,
    
    PasswordNotEqualsPasswordConfirm = 21,
    WrongUserPassword = 22,
    
    RoleAlreadyExists = 31,
    RoleNotFound = 32
    
}