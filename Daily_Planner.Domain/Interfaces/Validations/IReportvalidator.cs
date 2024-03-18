using Daily_Planner.Domain.Entity;
using Daily_Planner.Domain.Result;

namespace Daily_Planner.Domain.Interfaces.Validations;

public interface IReportvalidator : IBaseValidator<Report>
{
    /// <summary>
    /// Проверяется наличие отчета, если отчет с переданным названием есть в БД, то создать точно такой же нельзя
    /// Проверяется пользователь, если такой UserId не найден, соотвественно такого пользователя нет  
    /// </summary>
    /// <param name="report"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    BaseResult CreateValidator(Report report, User user);
}