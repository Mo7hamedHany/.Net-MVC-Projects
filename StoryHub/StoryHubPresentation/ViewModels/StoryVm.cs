using DAL.Models;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace StoryHubPresentation.ViewModels
{
    public class StoryVm
    {
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }

        public IFormFile? Image { get; set; }

        public string? ImageName { get; set; }

        public StoryTeller? Teller { get; set; }

        public int? TellerId { get; set; }
    }
}
