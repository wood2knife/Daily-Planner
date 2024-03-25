using AutoMapper;
using Daily_Planner.Domain.Dto.User;
using Daily_Planner.Domain.Entity;

namespace Daily_Planner.Application.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}