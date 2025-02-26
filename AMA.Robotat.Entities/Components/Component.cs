using AMA.Robotat.Entities.Robots;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.Robotat.Entities.Components
{
    public class Component
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        [Column(TypeName ="decimal(6,2)")]//9999.99
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<Robot> Robots { get; set; } = [];
    }
}
