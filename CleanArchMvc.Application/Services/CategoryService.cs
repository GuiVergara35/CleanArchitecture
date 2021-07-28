using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.Dto;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var categories = await _repository.GetCategories();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetById(int? id)
        {
            var category = await _repository.GetById(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task Add(CategoryDto categoryDto)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDto);
            await _repository.Create(categoryEntity);
        }

        public async Task Update(CategoryDto categoryDto)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDto);
            await _repository.Update(categoryEntity);
        }

        public async Task Remove(int? id)
        {
            var categoryEntity = _repository.GetById(id).Result;
            await _repository.Remove(categoryEntity);
        }
    }
}
