using EProductManagement.Data.Contexts;
using EProductManagement.Domain.Entities;
using EProductManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProductManagement.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PostgreSqlContext _context;
        public CategoryRepository(PostgreSqlContext context)
        {
            _context = context;
        }

        public async Task CreateCategory(Category Category)
        {
            await _context.Categories.AddAsync(Category);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteCategory(int Id)
        {
            var category = await GetById(Id);
            if (category != null)
            {
                category.DeleteTime = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Category>> GetByUpperCategoryId(int UpperCategoryId)
        {
            var categories = await _context.Categories
                .Include(x => x.EProducts)
                .Where(x => x.UpperCategoryId == UpperCategoryId)
                .ToListAsync();
            return categories;
        }

        public async Task<Category> GetById(int Id)
        {
            var category = await _context.Categories
                .Include(x => x.EProducts)
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();
            return category;
        }

        public async Task UpdateCategory(Category Category)
        {
            _context.Categories.Update(Category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetByMasterCategories()
        {
            var categories = await _context.Categories
                .Where(x => x.UpperCategoryId == 0)
                .ToListAsync();
            return categories;
        }
    }
}
