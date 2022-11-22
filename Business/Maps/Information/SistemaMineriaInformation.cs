using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Maps.Information
{
    class SistemaMineriaInformation
    {
        private readonly DataAccess.Firebase firebase;

        // Dato para conectarse con Firebase: Base de datos Missael
        /*private const string authSecret = "JzkVDa7KbVFCNL0Fs5LVENThlyxXYX5LbNHjuTZk";
        private const string basePath = "https://sistemamineria-15451-default-rtdb.firebaseio.com/";*/

        private const string authSecret = "ZLO4Jma5gliqw1EyRvuPpDWNvb7QR2cHltLdA43S";
        private const string basePath = "https://mvts-5855f-default-rtdb.firebaseio.com/";

        private const string nodoSemaforos = "intersecciones";
        private const string nodoCongestiones = "congestion";

        public SistemaMineriaInformation()
        {
            firebase = new DataAccess.Firebase(authSecret, basePath);
            firebase.Connect();
        }

        public List<MapObjects.Intersection> GetIntersections()
        {
            List<MapObjects.Intersection> intersections = new List<MapObjects.Intersection>();

            Dictionary<string, DataAccess.Entity.Interseccion> data = firebase.Select<Dictionary<string, DataAccess.Entity.Interseccion>>(nodoSemaforos);

            foreach (var item in data)
            {
                MapObjects.Intersection intersection = new MapObjects.Intersection(item.Value);

                intersections.Add(intersection);
            }

            return intersections;
        }

        public MapObjects.Intersection GetIntersection(string id)
        {
            DataAccess.Entity.Interseccion data = firebase.Select<DataAccess.Entity.Interseccion>(nodoSemaforos + "/" + id);

            if (data == null)
                throw new Exception("No se encontró un registro en la base de datos con el id " + id);

            MapObjects.Intersection intersection = new MapObjects.Intersection(data);

            return intersection;
        }

        //Test
        public void UpdateIntersection()
        {

        }

        public void UpdateSemaforo(string intersectionId, string direccion, string value)
        {
            MapObjects.Intersection intersection = GetIntersection(intersectionId);

            DataAccess.Entity.Interseccion semaforo = intersection.ConvertToEntity();

            semaforo.Semaforos.SetValue(direccion, value);

            firebase.Update(nodoSemaforos + "/" + intersectionId, semaforo);
        }

        public void UpdateCongestion(string value)
        {
            Congestion congestion = new Congestion() { estado = value };
            firebase.Update(nodoCongestiones, congestion);
        }

        class Congestion
        {
            public string estado { get; set; }
        }

        public void TurnRedSemaforo(string intersectionId, string direccion)
        {
            UpdateSemaforo(intersectionId, direccion, "2");
        }

        public void TurnGreenSemaforo(string intersectionId, string direccion)
        {
            UpdateSemaforo(intersectionId, direccion, "4");
        }

    }
}
