using EProductManagement.Data.Contexts;
using EProductManagement.Domain.Entities;
using EProductManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EProductManagement.Data.Repositories
{
    public class ProductBalanceRepository : IProductBalanceRepository
    {
        private readonly PostgreSqlContext _context;
        public Semaphore Semaphore = new Semaphore(1, 2);
        public ProductBalanceRepository(PostgreSqlContext context)
        {
            this._context = context;
        }
        public async Task CreateProductBalance(ProductBalance productBalance)
        {
            await _context.ProductBalances.AddAsync(productBalance);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductBalance>> GetProductBalancesByPartyId(int partyId)
        {
            return await _context.ProductBalances
                .Where(x => x.PartyId == partyId)
                .ToListAsync();
        }

        public async Task<ProductBalance> GetProductBalanceByPartyIdWithEProductId(int PartyId, int EProductId)
        {
            return await _context.ProductBalances
                .Include(x => x.EProduct)
                .Where(x => x.PartyId == PartyId && x.EProductId == EProductId)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateProductBalance(ProductBalance productBalance)
        {
            _context.ProductBalances.Update(productBalance);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductBalance>> GetProductBalancesByPartyIdWithEProductId(int SenderPartyId, int ReceiverPartyId, int EProductId)
        {
            return await _context.ProductBalances
                .Include(x => x.EProduct)
                .Where(x => (x.PartyId == SenderPartyId || x.PartyId == ReceiverPartyId) && x.EProductId == EProductId)
                .ToListAsync();
        }

    }
}
