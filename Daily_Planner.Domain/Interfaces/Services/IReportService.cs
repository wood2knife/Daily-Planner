using Daily_Planner.Domain.Dto;
using Daily_Planner.Domain.Result;

namespace Daily_Planner.Domain.Interfaces.Services;

/// <summary>
/// Сервис, отвечающий за работу с доменной части отчета (Report)
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Получение всех отчетов пользователя
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<CollectionResult<ReportDto>> GetReportsAsync(long userId);
}