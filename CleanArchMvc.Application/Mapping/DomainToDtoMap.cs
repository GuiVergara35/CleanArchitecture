using AutoMapper;
using CleanArchMvc.Application.Dto;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Mapping
{
    public class DomainToDtoMap : Profile
    {
        public DomainToDtoMap()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
