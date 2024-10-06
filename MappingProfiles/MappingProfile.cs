using SOPGraphQL.DTOs;

namespace SOPGraphQL.MappingProfiles;

using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
        CreateMap<Order, OrderDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
    }
}