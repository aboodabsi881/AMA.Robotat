using AMA.Robotat.Mvc.Models.Components;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMA.Robotat.Mvc.Models.Robots
{
    public class RobotDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(6,2)")]//9999.99
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<ComponentViewModel> Components { get; set; } = [];
    }
}
