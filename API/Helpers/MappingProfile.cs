using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<User, UserShowDto>();
    }
}