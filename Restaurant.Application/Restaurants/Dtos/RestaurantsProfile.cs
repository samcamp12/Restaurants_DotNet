using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantsProfile : Profile
{
    public RestaurantsProfile()
    {
        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(d => d.Address, opt => opt.MapFrom(
                src => new Address
                {
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode
                }
             ));
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(d => d.City, opt => opt.MapFrom(s => s.Address == null ? null : s.Address.City))
            .ForMember(d => d.Street, opt => opt.MapFrom(s => s.Address == null ? null : s.Address.Street))
            .ForMember(d => d.PostalCode, opt => opt.MapFrom(s => s.Address == null ? null : s.Address.PostalCode))
            .ForMember(d => d.Dishes, opt => opt.MapFrom(s => s.Dishes));

        CreateMap<UpdateRestaurantCommand, Restaurant>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

