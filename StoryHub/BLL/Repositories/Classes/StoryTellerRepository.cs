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
	public class StoryTellerRepository : GenericRepository<StoryTeller>, IStoryTellers
	{
        private readonly StoryDbContext _context;
		public StoryTellerRepository(StoryDbContext context) : base(context)
		{
            _context = context;

        }

        public async Task<IEnumerable<StoryTeller>> GetAllAsync(Expression<Func<StoryTeller, bool>> expression)
        {
            return await _context.StoryTellers.Where(expression).ToListAsync();
        }
    }
}
