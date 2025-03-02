using AMA.Robotat.Entities.Customers;
using AMA.Robotat.Entities.Robots;
using AMA.Robotat.Utils.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMA.Robotat.Mvc.Models.Orders
{
    public class CreateUpdateOrderViewModel
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public string? Note { get; set; }
        public Location Location { get; set; }
        public int CustomerId { get; set; }
        public List<int> RobotsIds { get; set; } = [];
        [ValidateNever]
        public SelectList CustomersLookup { get; set; }
        [ValidateNever]
        public MultiSelectList RobotLookup { get; set; }
    }
}
