using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml;
using DealerManagment.Data;
using DealerManagment.Models;
using DealerManagment.Models.InventoryModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealerManagment.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Current_Inventory()
        {
            var model = GetInventory(false);
            return View(model);
        }

        public IActionResult Sold_Inventory()
        {
            var model = GetInventory(true);

            // Calculate and store profit value
            double profit = model.Sum(i => i.Profit);
            ViewBag.Profit = profit.ToString("C", CultureInfo.CurrentCulture);

            return View(model);
        }

        // Get either the sold or current inventory
        private List<Inventory> GetInventory (bool sold)
        {
            IQueryable<Inventory> cars = null;
            IQueryable<Vehicle> carInfo = null;
        
            if (sold)
            {
                // Get Sold Inventory Data
                cars =  _context.Inventory.Where(p => p.Sold == true);
                carInfo = _context.Vehicles;
            } else
            {
                // Get Current Inventory Data
                 cars = _context.Inventory.Where(p => p.Sold == false); 
                 carInfo = _context.Vehicles;
            }
            
            // Init Inventory List
            var model = new List<Inventory>();

            // Add each Inventory Object From Database to the List
            foreach (var car in cars)
            {
                // Get detailed Vehicle info from Vehicle table
                car.Vehicle = carInfo.ToList().Find(x => x.VIN == car.VIN);
                model.Add(car);
            }

            return model;
        }
    }
}