using System.Diagnostics.CodeAnalysis;
using Daily_Planner.Application.Resources;
using Daily_Planner.Domain.Entity;
using Daily_Planner.Domain.Enum;
using Daily_Planner.Domain.Interfaces.Validations;
using Daily_Planner.Domain.Result;

namespace Daily_Planner.Application.Validations;

public class ReportValidator : IReportvalidator
{
    public BaseResult ValidateOnNull(Report model)
    {
        if (model == null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.ReportNotFound,
                ErrorCode = (int)ErrorCodes.ReportNotFound
            };
        }
        return new BaseResult();
    }

    public BaseResult CreateValidator(Report report, User user)
    {
        if (report != null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.ReportAlreadyExist,
                ErrorCode = (int)ErrorCodes.ReportAlreadyExist
            };
        }

        if (user == null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode = (int)ErrorCodes.UserNotFound
            };
        }

        return new BaseResult();
    }
}