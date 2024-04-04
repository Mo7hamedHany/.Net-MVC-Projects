using DAL.Models;

namespace StoryHubPresentation.ViewModels
{
	public class StoryTellerVm
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public int Rating { get; set; }
		public string? ImageName { get; set; }
		public IFormFile? Image { get; set; }
		//public ICollection<Story> Stories { get; set; } = new List<Story>();
	}
}
