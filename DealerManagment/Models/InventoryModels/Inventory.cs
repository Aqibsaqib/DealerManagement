using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DealerManagment.Models.InventoryModels
{
    [Table("Inventory")]
    public class Inventory
    {

        // PRIMARY KEY
        [Key]
        public int InventoryId { get; set; }

        // FOREIGN KEYS  
        [RegularExpression("[A-HJ-NPR-Z0-9]{13}[0-9]{4}", ErrorMessage = "Invalid Vehicle Identification Number Format.")]
        public string VIN { get; set; }  
        public string UserId { get; set; }

        [ForeignKey("VIN")]
        public Vehicle Vehicle { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        //ATTRIBUTES
        [Range(0, 999999, ErrorMessage ="Invalid mileage")]
        public int Mileage { get; set; }
        public string PurchaseDate { get; set; }
        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}")]
        public double PurchasePrice { get; set; }
        public string SoldDate { get; set; }
        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}")]
        public double SoldPrice { get; set; }
        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}")]
        public double Profit { get; set; }
        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}")]
        public bool Sold { get; set; }

     
    }
}
