//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ryding.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Point
    {
        public int PointID { get; set; }
        public Nullable<int> RouteID { get; set; }
        public Nullable<int> StopID { get; set; }
    
        public virtual StopPoint StopPoint { get; set; }
        public virtual Route Route { get; set; }
    }
}
