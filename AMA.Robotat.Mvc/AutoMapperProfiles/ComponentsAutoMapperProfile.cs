using AMA.Robotat.Entities.Components;
using AMA.Robotat.Mvc.Models.Components;
using AutoMapper;

namespace AMA.Robotat.Mvc.AutoMapperProfiles
{
    public class ComponentsAutoMapperProfile:Profile
    {
        public ComponentsAutoMapperProfile() { 
            CreateMap<Component,ComponentViewModel>();
            CreateMap<Component, ComponentDetailsViewModel>();
            CreateMap<CreateUpdateComponentViewModel,Component>().ReverseMap();
        }
    }
}
