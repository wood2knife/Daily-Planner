using AutoMapper;
using Daily_Planner.Domain.Dto.Report;
using Daily_Planner.Domain.Entity;

namespace Daily_Planner.Application.Mapping;

public class ReportMapping : Profile
{
    public ReportMapping()
    {
        CreateMap<Report, ReportDto>().ReverseMap();
    }
}