using EProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EProductManagement.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task CreateCategory(Category Category);
        Task<Category> GetById(int Id);
        Task<List<Category>> GetByUpperCategoryId(int CategoryId);
        Task<List<Category>> GetByMasterCategories();
        Task UpdateCategory(Category Category);
        Task DeleteCategory(int Id);
    }
}
