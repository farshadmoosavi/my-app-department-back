using System;
using Line.Models;

namespace Line.Repositories.Interfaces
{
	public interface ICurrencyRepository: IGenericRepository<Currency>
	{
        Task<Currency> GetCurrencyByName(string name);
        //Task<IEnumerable<Currency>> GetByOrganizationId(int organizationId);
        //Task<Currency> GetCurrencyByOrgIdAndCurrName(int organizationId, string name);
    }
}

