using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Maps.MapObjects
{
    public class Intersection
    {
        public enum Estado { Nulo, NoDefinido, Verde, Amarillo, Rojo }

        private static readonly int[,] DireccionesMatriz = { { 0, 1, 2 }, { 3, -1, 4 }, { 5, 6, 7 } };

        public double Latitud { get; private set; }
        public double Longitud { get; private set; }
        public string Id { get; set; }
        public bool[] DireccionesActivas { get; private set; }

        public Estado[,] Estados { get; private set; } = new Estado[3, 3];
        
        public Intersection(double latitud, double longitud)
        {
            Latitud = latitud;
            Longitud = longitud;
        }

        public Intersection(DataAccess.Entity.Interseccion entity)
        {
            double latitud = double.Parse(entity.Latitud);
            double longitud = double.Parse(entity.Longitud);
            string idn = entity.Id;
            bool[] direcciones = entity.Semaforos.DireccionesActivas();

            Latitud = latitud;
            Longitud = longitud;

            Id = idn;
            SetDireccionesActivas(direcciones);
            UpdateEstados(intToEstados(entity.Semaforos.GetEstados()));
        }

        public Estado[,] intToEstados(int[,] values)
        {
            Estado[,] estados = new Estado[3, 3];

            try
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        estados[i, j] = (Estado)values[i, j];
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception("Algo falló al tratar de convertir los estados.\n" + e.Message);
            }

            

            return estados;
        }

        public void SetDireccionesActivas(bool[] direcciones)
        {
            if (direcciones.Length != 8)
                throw new Exception("Se debe indicar las 8 direcciones que puede tener el semaforo correctamente.");

            DireccionesActivas = direcciones;

            bool[,] direccionesMatriz = new bool[,]{ { direcciones[0], direcciones[1], direcciones[2] },
                                                     { direcciones[3], false,          direcciones[4] },
                                                     { direcciones[5], direcciones[6], direcciones[7] } };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Estados[i, j] = (direccionesMatriz[i, j]) ? Estado.NoDefinido : Estado.Nulo;
                }
            }
        }

        public void Change(int i, int j)
        {
            if (Estados[i, j] != Estado.Nulo)
            {
                if (Estados[i, j] == Estado.Rojo)
                {
                    // Amarillo...

                    Estados[i, j] = Estado.Verde;
                }
                else if (Estados[i, j] == Estado.Verde)
                {
                    Estados[i, j] = Estado.Rojo;
                }
            }
        }

        public void UpdateEstados(Estado[,] estados)
        {
            if (estados.GetLength(0) != 3 || estados.GetLength(1) != 3)
                throw new Exception("Se intentó actualizar los estados de una interseccion incorrectamente. La matriz parametro de estados debe ser 3 x 3.");

            Estados = estados;
        }

        public void Update(int i, int j, Estado estado)
        {
            if (Estados[i, j] != Estado.Nulo)
            {
                Estados[i, j] = estado;
            }
        }

        public Estado DirectionState(int i, int j)
        {
            return Estados[i, j];
        }

        public DataAccess.Entity.Interseccion ConvertToEntity()
        {
            DataAccess.Entity.Interseccion entity = new DataAccess.Entity.Interseccion();

            entity.Id = Id;
            entity.Latitud = Latitud.ToString();
            entity.Longitud = Longitud.ToString();

            DataAccess.Entity.Semaforos direcciones = new DataAccess.Entity.Semaforos();
            direcciones.Sem0 = (Estados[0, 0] != Estado.Nulo) ? ((int)Estados[0, 0]).ToString() : null;
            direcciones.Sem1 = (Estados[0, 1] != Estado.Nulo) ? ((int)Estados[0, 1]).ToString() : null; 
            direcciones.Sem2 = (Estados[0, 2] != Estado.Nulo) ? ((int)Estados[0, 2]).ToString() : null;
            direcciones.Sem3 = (Estados[1, 0] != Estado.Nulo) ? ((int)Estados[1, 0]).ToString() : null;
            direcciones.Sem4 = (Estados[1, 2] != Estado.Nulo) ? ((int)Estados[1, 2]).ToString() : null;
            direcciones.Sem5 = (Estados[2, 0] != Estado.Nulo) ? ((int)Estados[2, 0]).ToString() : null;
            direcciones.Sem6 = (Estados[2, 1] != Estado.Nulo) ? ((int)Estados[2, 1]).ToString() : null;
            direcciones.Sem7 = (Estados[2, 2] != Estado.Nulo) ? ((int)Estados[2, 2]).ToString() : null;

            entity.Semaforos = direcciones;

            return entity;
        }

        public static int ConvertDirection(int i, int j)
        {
            return DireccionesMatriz[i, j];
        }

        public static void ConvertDirection(string value, out int i, out int j)
        {
            // Mejorar logica?

            i = -1;
            j = -1;

            switch(value)
            {
                case "0": i = 0; j = 0; break;
                case "1": i = 0; j = 1; break;
                case "2": i = 0; j = 2; break;
                case "3": i = 1; j = 0; break;
                case "4": i = 1; j = 2; break;
                case "5": i = 2; j = 0; break;
                case "6": i = 2; j = 1; break;
                case "7": i = 2; j = 2; break;
            }

        }
        
    }
}
