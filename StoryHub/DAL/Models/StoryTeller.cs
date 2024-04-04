

namespace DAL.Models
{
	public class StoryTeller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Rating { get; set; }
        public string? ImageName { get; set; }
		public ICollection<Story> Stories { get; set; } = new List<Story>();

        public override string ToString()
        {
            return Name;
        }
    }
}
