using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Semaforos
    {
        public string Sem0 { get; set; }
        public string Sem1 { get; set; }
        public string Sem2 { get; set; }
        public string Sem3 { get; set; }
        public string Sem4 { get; set; }
        public string Sem5 { get; set; }
        public string Sem6 { get; set; }
        public string Sem7 { get; set; }

        public bool[] DireccionesActivas()
        {
            bool[] result = new bool[8];

            result[0] = Sem0 != null;
            result[1] = Sem1 != null;
            result[2] = Sem2 != null;
            result[3] = Sem3 != null;
            result[4] = Sem4 != null;
            result[5] = Sem5 != null;
            result[6] = Sem6 != null;
            result[7] = Sem7 != null;

            return result;
        }

        public int[,] GetEstados()
        {
            int[,] estados = new int[3,3];

            estados[0, 0] = (Sem0 != null) ? int.Parse(Sem0) : 0;
            estados[0, 1] = (Sem1 != null) ? int.Parse(Sem1) : 0;
            estados[0, 2] = (Sem2 != null) ? int.Parse(Sem2) : 0;
            estados[1, 0] = (Sem3 != null) ? int.Parse(Sem3) : 0;
            estados[1, 1] = 0;
            estados[1, 2] = (Sem4 != null) ? int.Parse(Sem4) : 0;
            estados[2, 0] = (Sem5 != null) ? int.Parse(Sem5) : 0;
            estados[2, 1] = (Sem6 != null) ? int.Parse(Sem6) : 0;
            estados[2, 2] = (Sem7 != null) ? int.Parse(Sem7) : 0;

            return estados;
        }

        public void SetValue(string direccion, string value)
        {
            switch(direccion)
            {
                case "0": Sem0 = value; break;
                case "1": Sem1 = value; break;
                case "2": Sem2 = value; break;
                case "3": Sem3 = value; break;
                case "4": Sem4 = value; break;
                case "5": Sem5 = value; break;
                case "6": Sem6 = value; break;
                case "7": Sem7 = value; break;

            }
        }
    }
}
