using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		public IStoryTellers Tellers { get; }

		public IStory Stories { get; }

		 Task<int> CompleteAsync();
	}
}
