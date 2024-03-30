using AutoMapper;
using Daily_Planner.Domain.Dto.Role;
using Daily_Planner.Domain.Entity;

namespace Daily_Planner.Application.Mapping;

public class RoleMapping : Profile
{
    public RoleMapping()
    {
        CreateMap<Role, RoleDto>().ReverseMap();
    }
}