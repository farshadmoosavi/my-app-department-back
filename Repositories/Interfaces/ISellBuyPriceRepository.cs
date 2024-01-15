using System;
using Line.Models;

namespace Line.Repositories.Interfaces
{
	public interface ISellBuyPriceRepository: IGenericRepository<SellBuyPrice>
	{
        Task DeleteSellBuyPriceByUpdate(int SellBuyPriceId);
        Task<IEnumerable<SellBuyPrice>> GetAllNonDeletedSellBuyPrices();
        Task<IEnumerable<SellBuyPrice>> GetAllDeletedSellBuyPrices();
    }
}

