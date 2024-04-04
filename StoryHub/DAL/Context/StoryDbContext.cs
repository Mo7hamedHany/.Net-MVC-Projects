using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class StoryDbContext : DbContext
    {
        public DbSet<Story> Stories { get; set; }
        public DbSet<StoryTeller> StoryTellers { get; set; }

        public StoryDbContext(DbContextOptions<StoryDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
