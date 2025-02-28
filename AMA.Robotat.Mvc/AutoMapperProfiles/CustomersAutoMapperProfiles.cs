using AMA.Robotat.Entities.Customers;
using AMA.Robotat.Mvc.Models.Customers;
using AutoMapper;

namespace AMA.Robotat.Mvc.AutoMapperProfiles
{
    public class CustomersAutoMapperProfiles:Profile
    {
        public CustomersAutoMapperProfiles() 
        {
            CreateMap<Customer,CustomerViewModel>();
            CreateMap<Customer, CustomerDetailsViewModel>();
            CreateMap<Customer, CreateUpdateCustomerViewModel>().ReverseMap();
        }
    }
}
