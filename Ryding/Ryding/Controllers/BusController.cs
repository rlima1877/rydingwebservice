using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using Ryding.Data;
using Ryding.Models;

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
        public async Task<ActionResult> GetAllBuses()
        {
            List<Bus> buses = new List<Bus>();
            buses = await db.Buses.ToListAsync();
            List<BusModel> busModels = new List<BusModel>();

            if(buses.Count > 0)
            {
                foreach(var b in buses)
                {
                    BusModel bus = new BusModel();
                    bus.BusID = b.BusID;
                    bus.BusNumber = b.BusNumber;
                    bus.Direction = b.Direction.Type;
                    bus.Latitude = (double)b.Latitude;
                    bus.Longitude = (double)b.Longitude;
                    busModels.Add(bus);
                } 
            }

            return Json(new { status = true, buses = busModels }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> SetBusLocation(int id, double lat, double lon, string direction)
        {
            if(direction.IndexOf("inbound", StringComparison.OrdinalIgnoreCase) < 0 && direction.IndexOf("outbound", StringComparison.OrdinalIgnoreCase) < 0)
            {
                return Json(new { status = false, message = "Bus direction input is not valid." }, JsonRequestBehavior.AllowGet);
            }

            Bus bus = new Bus();
            var rydingBus = await db.Buses.Where(b => b.BusID == id).ToListAsync();
            if(rydingBus.Count == 0)
            {
                return Json(new { status = false, message = "Bus with ID " + id + " cannot be found." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                rydingBus[0].Latitude = lat;
                rydingBus[0].Longitude = lon;
                if (direction.Contains("inbound"))
                    rydingBus[0].DirectionID = 1;
                else
                    rydingBus[0].DirectionID = 2;
                await db.SaveChangesAsync();
                return Json(new { status = "success", message = "Location of bus number " + rydingBus[0].BusNumber + " is set to (" + lat + ", " + lon + ")"   }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetBusLocation(int id)
        {
            var bus = await db.Buses.Where(b => b.BusID == id).FirstOrDefaultAsync();
            if(bus != null)
            {
                return Json(new { Latitude = bus.Latitude, Longitude = bus.Longitude }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetBusRoute(int BusID)
        {
            var points = await db.Points.Where(p => p.Route.BusID == BusID).ToListAsync();
            List<PointModel> PointList = new List<PointModel>();
            foreach (var point in points)
            {
                PointModel Point = new PointModel();
                Point.BusID = (int)point.Route.BusID;
                Point.Latitude = (double)point.StopPoint.Latitude;
                Point.Longitude = (double)point.StopPoint.Longitude;
                Point.Name = point.StopPoint.Name;
                PointList.Add(Point);
            }
            if(points != null)
            {
                return Json(new { status = true, points = PointList }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }
    }
}
