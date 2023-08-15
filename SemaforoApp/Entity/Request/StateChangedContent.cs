using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemaforoApp.Entity.Request
{
    class StateChangedContent
    {
        public string Id { get; set; }
        public string IntersectionId { get; set; }
        public string Clave { get; set; }
    }
}
