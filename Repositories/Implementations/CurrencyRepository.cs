using System;
using Line.Data;
using Line.Models;
using Line.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Line.Repositories.Implementations
{
	public class CurrencyRepository : GenericRepository<Currency>, ICurrencyRepository
    {
        private readonly LineDbContext _dbContext;

        public CurrencyRepository(LineDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Currency> GetCurrencyByName(string name)
        {
            var CurrencyToFind = await _dbContext.Currencies
                    .FirstOrDefaultAsync(u => u.CurrencyName == name);

            return CurrencyToFind;
        }
        //public async Task<IEnumerable<Currency>> GetByOrganizationId(int organizationId)
        //{
        //    var CurrencyToFind = await _dbContext.Currencies
        //            .Where(u => u.OrganizationId == organizationId).ToListAsync();

        //    return CurrencyToFind;
        //}
        //public async Task<Currency> GetCurrencyByOrgIdAndCurrName(int organizationId, string Name)
        //{
        //    var CurrencyToFind = await _dbContext.Currencies
        //            .FirstOrDefaultAsync(u => u.OrganizationId == organizationId && u.CurrencyName == Name);

        //    return CurrencyToFind;
        //}

    }
}

