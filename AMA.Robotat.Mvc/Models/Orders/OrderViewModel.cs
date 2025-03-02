using AMA.Robotat.Entities.Customers;
using AMA.Robotat.Entities.Robots;
using AMA.Robotat.Utils.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMA.Robotat.Mvc.Models.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public Location Location { get; set; }

        [ValidateNever]
        public SelectList CustomersLookup { get; set; }
    }
}
