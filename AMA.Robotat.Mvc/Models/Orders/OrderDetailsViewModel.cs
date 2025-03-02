using AMA.Robotat.Entities.Customers;
using AMA.Robotat.Entities.Robots;
using AMA.Robotat.Mvc.Models.Robots;
using AMA.Robotat.Utils.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMA.Robotat.Mvc.Models.Orders
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal TotalPrice { get; set; }
        public string CustomerFullName { get; set; }
        public string? Note { get; set; }
        public Location Location { get; set; }
        public List<RobotViewModel> Robots { get; set; } = [];
    }
}
