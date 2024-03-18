using Daily_Planner.Application.Resources;
using Daily_Planner.Domain.Dto;
using Daily_Planner.Domain.Entity;
using Daily_Planner.Domain.Enum;
using Daily_Planner.Domain.Interfaces.Repositories;
using Daily_Planner.Domain.Interfaces.Services;
using Daily_Planner.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Daily_Planner.Application.Services;

public class ReportService : IReportService
{
    private readonly IBaseRepository<Report> _reportRepository;
    private readonly ILogger _logger;

    public ReportService(IBaseRepository<Report> reportRepository)
    {
        _reportRepository = reportRepository;
    }
    /// <inheritdoc />      
    public async Task<CollectionResult<ReportDto>> GetReportsAsync(long userId)
    {
        ReportDto[] reports;
        try
        {
            reports = await _reportRepository.GetAll()
                .Where(x => x.UserId == userId)
                .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                .ToArrayAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new CollectionResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }

        if (!reports.Any())
        {
            _logger.Warning(ErrorMessage.ReportsNotfound, reports.Length);
            return new CollectionResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.ReportsNotfound,
                ErrorCode = (int)ErrorCodes.ReportsNotFound
            };
        }

        return new CollectionResult<ReportDto>()
        {
            Data = reports,
            Count = reports.Length
        };
    }
}