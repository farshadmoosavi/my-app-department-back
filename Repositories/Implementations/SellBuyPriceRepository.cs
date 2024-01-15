using System;
using Line.Data;
using Line.Models;
using Line.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Line.Repositories.Implementations
{
	public class SellBuyPriceRepository : GenericRepository<SellBuyPrice>, ISellBuyPriceRepository
    {
        private readonly LineDbContext _dbContext;

        public SellBuyPriceRepository(LineDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SellBuyPrice>> GetAllNonDeletedSellBuyPrices()
        {
            var sellBuyPrices = await _dbContext.SellBuyPrices
                .Where(S =>  S.Deleted == false)
                .ToListAsync();
            return sellBuyPrices;
        }

        public async Task<IEnumerable<SellBuyPrice>> GetAllDeletedSellBuyPrices()
        {
            var sellBuyPrices = await _dbContext.SellBuyPrices
                .Where(S => S.Deleted == true)
                .ToListAsync();
            return sellBuyPrices;
        }

        public async Task DeleteSellBuyPriceByUpdate(int sellBuyPriceId)
        {
            // Retrieve the user from the database
            var sellBuyPrice = await _dbContext.SellBuyPrices.FindAsync(sellBuyPriceId);

            if (sellBuyPrice != null)
            {
                // Set the Deleted property to true instead of physically deleting
                sellBuyPrice.Deleted = true;

                // Mark the entity as modified
                _dbContext.Entry(sellBuyPrice).State = EntityState.Modified;

                // Save changes
                await SaveChangesAsync();
            }
            // Handle the case where the user with the given ID is not found (optional)
            else
            {
                // You can throw an exception, return a specific response, or handle it based on your requirements.
                // For simplicity, this example returns a NotFound response.
                Console.WriteLine($"sellBuyPrice with ID {sellBuyPriceId} not found.");
            }
        }

    }
}

