using AMA.Robotat.Entities.Orders;
using AMA.Robotat.Mvc.Models.Components;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMA.Robotat.Mvc.Models.Robots
{
    public class CreateUpdateRobotViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name="Components")]
        public List<int> ComponentsIds { get; set; } = [];

        [ValidateNever]
        public MultiSelectList ComponentLookup { get; set; }
    }
}
