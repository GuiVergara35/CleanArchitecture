using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchMvc.Application.Dto;

namespace CleanArchMvc.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task<CategoryDto> GetById(int? id);
        Task Add(CategoryDto categoryDto);
        Task Update(CategoryDto categoryDto);
        Task Remove(int? id);
    }
}
