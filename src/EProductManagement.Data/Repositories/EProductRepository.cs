using EProductManagement.Data.Contexts;
using EProductManagement.Domain.Entities;
using EProductManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EProductManagement.Data.Repositories
{
    public class EProductRepository : IEProductRepository
    {
        private readonly PostgreSqlContext _context;

        public EProductRepository(PostgreSqlContext context)
        {
            this._context = context;
        }

        public async Task CreateEProduct(EProduct eProduct)
        {
            await _context.EProducts.AddAsync(eProduct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEproduct(int Id)
        {
            var eProduct = await GetByEProductId(Id);
            if (eProduct != null)
            {
                eProduct.DeleteTime = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<EProduct> GetByComfirmedEProductId(int id)
        {
            return await _context.EProducts
                .Include(e => e.Category)
                .Where(e => e.Id == id && e.DeleteTime == null && e.IsApproved == true)
                .FirstOrDefaultAsync();
        }

        public async Task<EProduct> GetByEProductId(int id)
        {
            return await _context.EProducts
                .Include(e => e.Category)
                .Where(e => e.Id == id && e.DeleteTime == null)
                .FirstOrDefaultAsync();
        }



        public async Task<List<EProduct>> GetByMerchantId(int MerchantId)
        {
            var eProduct = await _context.EProducts
                .Where(x => x.MerchantId == MerchantId && x.DeleteTime == null).ToListAsync();

            return eProduct;
        }

        public async Task<List<EProduct>> GetEProducts()
        {
           var eProducts = await _context.EProducts.Where(x => x.DeleteTime == null).ToListAsync();

            return eProducts;
        }

        public async Task UpdateEproduct(EProduct eProduct)
        {
            _context.EProducts.Update(eProduct);
            await _context.SaveChangesAsync();
        }
    }
}
