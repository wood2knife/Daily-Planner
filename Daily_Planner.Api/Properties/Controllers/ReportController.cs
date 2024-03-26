using Asp.Versioning;
using Daily_Planner.Domain.Dto.Report;
using Daily_Planner.Domain.Interfaces.Services;
using Daily_Planner.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Daily_Planner.Api.Properties.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    /// <summary>
    /// Get all reports from user
    /// </summary>
    /// <param name="userId"></param>
    /// <remarks>
    /// Request for getting report from user
    ///
    ///     GET
    ///     {
    ///         "userId" : 1,
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Success response</response>
    /// <response code="400">Bad request</response>
    [HttpGet("reports/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> GetUserReports(long userId)
    {
        var response = await _reportService.GetReportsAsync(userId);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Get report by id method
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Request for getting report
    ///
    ///     GET
    ///     {
    ///         "userId" : 1,
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Success response</response>
    /// <response code="400">Bad request</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> GetReport(long id)
    {
        var response = await _reportService.GetReportByIdAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Delete report method
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Request for deleting report
    ///
    ///     DELETE
    ///     {
    ///         "userId" : 1,
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Success response</response>
    /// <response code="400">Bad request</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> DeleteReport(long id)
    {
        var response = await _reportService.DeleteReportAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    /// <summary>
    /// Create report method
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Request for creating report
    ///
    ///     POST
    ///     {
    ///         "name" : "Report #1",
    ///         "description: "Test Report",
    ///         "userId" : 1
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Success response</response>
    /// <response code="400">Bad request</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> CreateReport([FromBody]CreateReportDto dto)
    {
        var response = await _reportService.CreateReportAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Update report method
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Request for updating report
    ///
    ///     PUT
    ///     {
    ///         "userId" : 1,
    ///         "name" : "Updated report name",
    ///         "description: "Updated report description",
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Success response</response>
    /// <response code="400">Bad request</response>
    [HttpPut]
    public async Task<ActionResult<BaseResult<ReportDto>>> Updatereport([FromBody] UpdateReportDto dto)
    {
        var response = await _reportService.UpdateReportAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}