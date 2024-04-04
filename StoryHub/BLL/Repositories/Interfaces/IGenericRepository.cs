using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync();
		//Task<T> FindAsync(int id);
		Task<T?> GetAsync(int id);
		Task AddAsync(T entity);
		void DeleteAsync(T entity);
		void UpdateAsync(T entity);
	}
}
