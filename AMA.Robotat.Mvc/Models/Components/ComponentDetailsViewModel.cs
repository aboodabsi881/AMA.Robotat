using AMA.Robotat.Entities.Robots;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMA.Robotat.Mvc.Models.Components
{
    public class ComponentDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(6,2)")]//9999.99
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
