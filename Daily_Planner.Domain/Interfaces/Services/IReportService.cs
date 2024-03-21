using Daily_Planner.Domain.Dto.Report;
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

    /// <summary>
    /// Получение отчета по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> GetReportByIdAsync(long id);

    /// <summary>
    /// Создание отчета с базовыми параметрами
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto);

    /// <summary>
    /// Удаление отчета
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> DeleteReportAsync(long id);
    
    /// <summary>
    /// Обновление отчета
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto);
}