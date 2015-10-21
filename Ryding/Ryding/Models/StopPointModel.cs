using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ryding.Models
{
    public class StopPointModel
    {
        public int PointID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public int BusID { get; set; }
    }
}