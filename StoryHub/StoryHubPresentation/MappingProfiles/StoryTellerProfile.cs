using AutoMapper;
using DAL.Models;
using StoryHubPresentation.ViewModels;

namespace StoryHubPresentation.MappingProfiles
{
    public class StoryTellerProfile : Profile
    {
        public StoryTellerProfile()
        {
            CreateMap<StoryTellerVm,StoryTeller>().ReverseMap();
        }
    }
}
