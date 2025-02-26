using AMA.Robotat.Entities.Components;
using AMA.Robotat.Entities.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.Robotat.Entities.Robots
{
    public class Robot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(6,2)")]//9999.99
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<Component> Components { get; set; } = [];
        public List<Order> Orders { get; set; }
    }
}
