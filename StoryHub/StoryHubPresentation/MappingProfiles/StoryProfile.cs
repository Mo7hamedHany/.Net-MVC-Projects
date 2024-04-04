using AutoMapper;
using DAL.Models;
using StoryHubPresentation.ViewModels;

namespace StoryHubPresentation.MappingProfiles
{
    public class StoryProfile : Profile
    {
        public StoryProfile() 
        {
            CreateMap<Story,StoryVm>().ReverseMap();
        }
    }
}
