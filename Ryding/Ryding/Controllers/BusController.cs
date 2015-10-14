using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using Ryding.Data;

namespace Ryding.Controllers
{
    public class BusController : Controller
    {
        Entities db = new Entities();

        // GET: Map
        public async Task<ActionResult> Map()
        {
            List<Bus> Buses = await db.Buses.ToListAsync();
            ViewBag.Buses = Buses;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> SetBusLocation(string num, double lat, double lon)
        {
            Bus bus = new Bus();
            var rydingBus = await db.Buses.Where(b => b.BusNumber == num).ToListAsync();
            if(rydingBus.Count == 0)
            {
                bus.BusNumber = num;
                bus.Latitude = lat;
                bus.Longitude = lon;
                db.Buses.Add(bus);
                await db.SaveChangesAsync();
            }
            else
            {
                rydingBus[0].BusNumber = num;
                rydingBus[0].Latitude = lat;
                rydingBus[0].Longitude = lon;
                await db.SaveChangesAsync();
                return Json(new { status = "success", message = "Locaiton of bus number " + num + " is set to (" + lat + ", " + lon + ")"   }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetBusLocation(string num)
        {
            var bus = await db.Buses.Where(b => b.BusNumber == num).FirstOrDefaultAsync();
            if(bus != null)
            {
                return Json(new { Latitude = bus.Latitude, Longitude = bus.Longitude }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }
    }
}