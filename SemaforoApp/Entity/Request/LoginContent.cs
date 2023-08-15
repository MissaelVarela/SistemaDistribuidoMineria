using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemaforoApp.Entity.Request
{
    class LoginContent
    {
        public string id { get; set; }
        public string intersectionId { get; set; }
        public string pass { get; set; }
    }
}
