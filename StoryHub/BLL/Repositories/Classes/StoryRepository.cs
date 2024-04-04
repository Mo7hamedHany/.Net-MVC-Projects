using BLL.Repositories.Interfaces;
using DAL.Context;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Classes
{
    public class StoryRepository : GenericRepository<Story>, IStory
    {
        public StoryRepository(StoryDbContext context) : base(context)
        {
        }
    }
}
