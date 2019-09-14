using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DealerManagment.Models
{
    [Table("Vehicle")]
    public class Vehicle
    {
        [Key]
        [Required]
        public string VIN { get; set; }

        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Mileage { get; set; }

        public Vehicle(string vIN, int year, string make, string model)
        {
            VIN = vIN;
            Year = year;
            Make = make;
            Model = model;
        }
    }
}
