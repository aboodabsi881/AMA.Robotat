using AMA.Robotat.Entities.Robots;
using AMA.Robotat.Mvc.Models.Robots;
using AutoMapper;

namespace AMA.Robotat.Mvc.AutoMapperProfiles
{
    public class RobotsAutoMapperProfiles :Profile
    {
        public RobotsAutoMapperProfiles() 
        {
            CreateMap<Robot, RobotViewModel>();
            CreateMap<Robot, RobotDetailsViewModel>();
            CreateMap<CreateUpdateRobotViewModel, Robot>().ReverseMap()
                .ForMember(createUpdateRobotViewModel => createUpdateRobotViewModel
                                    .ComponentsIds
                                          , opts => 
                                                opts.MapFrom(robot => robot.Components.Select(robot => robot.Id)));
        }
    }
}
