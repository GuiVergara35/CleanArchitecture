using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.Dto;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using MediatR;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var productsQuery = new GetProductsQuery();

            if (productsQuery == null)
                throw new ArgumentNullException($"Entity could not be loaded");

            var result = await _mediator.Send(productsQuery);

            return _mapper.Map<IEnumerable<ProductDto>>(result);
        }

        public async Task<ProductDto> GetById(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if (productByIdQuery == null)
                throw new Exception($"Entity couçd not be loaded");

            var result = await _mediator.Send(productByIdQuery);
            return _mapper.Map<ProductDto>(result);
        }

        // public async Task<ProductDto> GetProductCategory(int? id)
        // {
        //     var productByIdQuery = new GetProductByIdQuery(id.Value);

        //     if (productByIdQuery == null)
        //         throw new Exception($"Entity couçd not be loaded");

        //     var result = await _mediator.Send(productByIdQuery);
        //     return _mapper.Map<ProductDto>(result);
        // }

        public async Task Add(ProductDto productDto)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDto);
            await _mediator.Send(productCreateCommand);
        }

        public async Task Update(ProductDto productDto)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDto);
            await _mediator.Send(productUpdateCommand);
        }

        public async Task Remove(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);
            if (productRemoveCommand == null)
                throw new Exception($"Entity could not be loaded");

            await _mediator.Send(productRemoveCommand);
        }
    }
}
