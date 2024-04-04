using BLL.Repositories.Classes;
using BLL.Repositories.Interfaces;
using DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace StoryHubPresentation
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StoryDbContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("default")); });

            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            builder.Services.AddScoped<IStoryTellers, StoryTellerRepository>();

            builder.Services.AddScoped<IStory, StoryRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}