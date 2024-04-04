using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
	public interface IStoryTellers : IGenericRepository<StoryTeller>
	{
        Task<IEnumerable<StoryTeller>> GetAllAsync(Expression<Func<StoryTeller, bool>> expression);
    }
}
