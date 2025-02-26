using AMA.Robotat.Entities.Customers;
using AMA.Robotat.Entities.Robots;
using AMA.Robotat.Utils.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.Robotat.Entities.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal TotalPrice{ get; set; }
        public int CustomerId { get; set; }
        public Customer Costomer { get; set; }
        public string? Note { get; set; }
        public Location Location { get; set; }
        public List<Robot> Robots { get; set; } = [];
    }
}
