using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Maps.Connections.Entity.Response
{
    class DataChangedContent
    {
        public string Id { get; set; }
        public string IntersectionId { get; set; }
        public byte State { get; set; }
    }
}
