using BLL.Repositories.Interfaces;
using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Classes
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly StoryDbContext _context;

		public GenericRepository(StoryDbContext context) 
		{
			_context = context;
		}

		public async Task AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
		}

		public  void DeleteAsync(T entity)
		{
			_context.Set<T>().Remove(entity);
		}

		//public Task<T> FindAsync(int id)
		//{
		//	throw new NotImplementedException();
		//}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			if (typeof(T) == typeof(Story)) 
				return (IEnumerable<T>) await _context.Stories.Include(s => s.Teller).ToListAsync();

			return await _context.Set<T>().ToListAsync();
		}


        public async Task<T?> GetAsync(int id)
        {
            // Special handling for the Story type
            if (typeof(T) == typeof(Story))
            {
                // Assuming you cast your DbSet to the specific type when needed
                var storyQuery = _context.Set<Story>().Include(s => s.Teller);
                // Use FirstOrDefaultAsync instead of FindAsync to allow for Include
                return (T?)(object)await storyQuery.FirstOrDefaultAsync(s => s.Id == id);
            }

            // Default handling for other types
            return await _context.Set<T>().FindAsync(id);
        }


        public void UpdateAsync(T entity)
		{
			_context.Set<T>().Update(entity);
		}
	}
}
