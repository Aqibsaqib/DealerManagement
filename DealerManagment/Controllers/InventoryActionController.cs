using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using DealerManagment.Data;
using DealerManagment.Models;
using DealerManagment.Models.InventoryModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealerManagment.Controllers
{
    public class InventoryActionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryActionController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Decode_VIN()
        {
            ViewData["UserId"] = _context.GetUserId();
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Inventory.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            vehicle.Vehicle = _context.Vehicles.Find(vehicle.VIN);
            return View(vehicle);
        }

        public async Task<IActionResult> Sell(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Inventory.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            vehicle.Vehicle = _context.Vehicles.Find(vehicle.VIN);

            return View(vehicle);
        }

        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }


            var inventoryItem = await _context.Inventory
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InventoryId == id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

    
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(inventoryItem);
        }

        [HttpPost]
        public async Task<IActionResult> Decode_VIN([Bind("VIN, UserId")] Inventory inventoryItem)
        {
            if (ModelState.IsValid)
            {
                inventoryItem.Vehicle = GetApiData(inventoryItem.VIN);


                //add to databases
                _context.Add(inventoryItem.Vehicle);
                _context.Add(inventoryItem);
                await _context.SaveChangesAsync();

                return RedirectToAction("Current_Inventory", "Inventory");
            }

            return View(inventoryItem);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            // Find the car to be edited from the inventory
            var carToUpdate = await _context.Inventory.FirstOrDefaultAsync(c => c.InventoryId == id);

            // Update the information if needed
            if (await TryUpdateModelAsync<Inventory>(
                carToUpdate,
                "",
                c => c.Mileage, c => c.PurchaseDate, c => c.PurchasePrice))
            {
                try

                {
                    // Save changes and return to main menu
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Current_Inventory", "Inventory");
                }
                catch (DbUpdateException)
                {
                    throw;
                }
            }
            return View(carToUpdate);
        }

        [HttpPost, ActionName("Sell")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SellPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find the car to be edited from the inventory
            var carToUpdate = await _context.Inventory.FirstOrDefaultAsync(c => c.InventoryId == id);
            carToUpdate.Sold = true;

            // Update the information if needed
            if (await TryUpdateModelAsync<Inventory>(
                carToUpdate,
                "",
                c => c.Mileage, c => c.SoldDate, c => c.SoldPrice))
            {
                // Calcuate and update profit
                carToUpdate.Profit = carToUpdate.SoldPrice - carToUpdate.PurchasePrice;

                try
                {
                    // Save changes and return to main menu
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Current_Inventory", "Inventory");
                }
                catch (DbUpdateException)
                {
                    throw;
                }
            }
            return View(carToUpdate);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            var inventoryItem = await _context.Inventory.FindAsync(id);
            var vehicle = await _context.Vehicles.FindAsync(inventoryItem.VIN);
            if (inventoryItem == null || vehicle == null)
            {
                return RedirectToAction("Current_Inventory", "Inventory");
            }

            try
            {
                _context.Inventory.Remove(inventoryItem);
                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction("Current_Inventory", "Inventory");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        // Helper Method For Decoding VIN Numbers
        private Vehicle GetApiData(string vin)
        {
            string url = $"https://vpic.nhtsa.dot.gov/api/vehicles/decodevinvaluesextended/{vin}?format=xml";
            string make = "", model = "", year = "";

            // Call api and get xml document
            XmlDocument doc = new XmlDocument();
            doc.Load(url);
            XmlNodeList nodeList = doc.SelectNodes("Response/Results/DecodedVINValues");

            // select the needed properties
            foreach (XmlNode xn in nodeList)
            {
                make = xn["Make"].InnerText;
                model = xn["Model"].InnerText;
                year = xn["ModelYear"].InnerText;
            }
            int yearInt = Int32.Parse(year);

            Vehicle vehicle = new Vehicle(vin, yearInt, make, model);

            return vehicle;
        }


    }
}