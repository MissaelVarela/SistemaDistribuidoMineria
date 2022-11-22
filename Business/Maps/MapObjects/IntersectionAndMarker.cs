using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Maps.MapObjects
{
    internal class IntersectionAndMarker
    {
        public Intersection interseccion;
        public GMap.NET.WindowsForms.GMapMarker marker;

        public IntersectionAndMarker(Intersection intersection, GMap.NET.WindowsForms.GMapMarker marker)
        {
            this.interseccion = intersection;
            this.marker = marker;
        }
    }
}
