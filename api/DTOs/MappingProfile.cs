using api.Controllers.Users.v1;
using api.DTOs.Auth.Requests;
using api.DTOs.Users;
using api.Models;
using AutoMapper;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.fullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.jobTitle,
                opt => opt.MapFrom(src => src.JobTitle));
        CreateMap<User, UserWithRolesDto>()
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.JobTitle,
                opt => opt.MapFrom(src => src.JobTitle));
    }
}
