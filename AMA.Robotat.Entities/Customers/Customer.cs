using AMA.Robotat.Entities.Orders;
using AMA.Robotat.Utils.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.Robotat.Entities.Customers
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<Order> Orders { get; set; } = [];

        [NotMapped]
        public string FullName {
            get
            { 
                return $"{FirstName}  {LastName}";
            } 
        }
        [NotMapped]
        public int Age
        {
            get
            {
                return DateTime.Now.Year - DateOfBirth.Year;
            }
        }
    }
}
