using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Interseccion
    {
        public string Id { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public Semaforos Semaforos { get; set; }
    }
}
