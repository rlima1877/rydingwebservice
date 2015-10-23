using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ryding.Models
{
    public class BusModel
    {
        public int BusID { get; set; }
        public string BusNumber { get; set; }
        public string Direction { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}