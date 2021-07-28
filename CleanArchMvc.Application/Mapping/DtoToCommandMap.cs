using AutoMapper;
using CleanArchMvc.Application.Dto;
using CleanArchMvc.Application.Products.Commands;

namespace CleanArchMvc.Application.Mapping
{
    public class DtoToCommandMap : Profile
    {
        public DtoToCommandMap()
        {
            CreateMap<ProductDto, ProductCreateCommand>();
            CreateMap<ProductDto, ProductUpdateCommand>();
        }
    }
}
