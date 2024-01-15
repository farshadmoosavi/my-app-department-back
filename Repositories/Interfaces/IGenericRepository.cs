using System;
using Line.Models;

namespace Line.Repositories.Interfaces 
{
	public interface IGenericRepository<T>
	{
        Task<T> GetById(int id);
        Task<T> Create(T t);
        Task Update(T t);
        Task Delete(int id);
        Task<IEnumerable<T>> GetAll();
    }
}

