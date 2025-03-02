using AMA.Robotat.Entities.Customers;
using AMA.Robotat.Entities.Orders;
using AMA.Robotat.Entities.Robots;
using AMA.Robotat.Mvc.Models.Orders;
using AutoMapper;
using System.Linq;

namespace AMA.Robotat.Mvc.AutoMapperProfiles
{
    public class OrdersAutoMapperProfiles : Profile
    {
        public OrdersAutoMapperProfiles()
        {
            // Mapping from Order to ViewModels
            CreateMap<Order, OrderViewModel>();
            CreateMap<Order, OrderDetailsViewModel>();
            CreateMap<Order, CreateUpdateOrderViewModel>().ReverseMap();

        }
    }
}