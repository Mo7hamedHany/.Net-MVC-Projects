using BLL.Repositories.Interfaces;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Classes
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly Lazy<IStoryTellers> _storyTellers;
		private readonly Lazy<IStory> _stories;
		private readonly StoryDbContext _context;

        public UnitOfWork(StoryDbContext context, IStoryTellers storyTellers, IStory stories)
        {
            _context = context;
            _storyTellers = new Lazy<IStoryTellers>(new StoryTellerRepository(context));
            _stories = new Lazy<IStory>(new StoryRepository(context));
        }

        public IStoryTellers Tellers => _storyTellers.Value;
		public IStory Stories => _stories.Value;

		public async Task<int> CompleteAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public async ValueTask DisposeAsync()
		{
			 await _context.DisposeAsync();
		}
	}
}
