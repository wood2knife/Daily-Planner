using Daily_Planner.Domain.Result;

namespace Daily_Planner.Domain.Interfaces.Validations;

public interface IBaseValidator<in T> where T : class
{
    BaseResult ValidateOnNull(T model);
}