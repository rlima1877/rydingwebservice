﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ryding.Models
{
    public class PointModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public int BusID { get; set; }
    }
}