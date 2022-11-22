using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace DataAccess
{
    public class Firebase
    {
        private readonly string authSecret;
        private readonly string basePath;

        private IFirebaseConfig config;
        private IFirebaseClient client;

        public bool IsConnected { get; private set; }

        public Firebase(string authSecret, string basePath)
        {
            this.authSecret = authSecret;
            this.basePath = basePath;
        }

        public void Connect()
        {
            config = new FirebaseConfig
            {
                AuthSecret = authSecret,
                BasePath = basePath
            };

            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                IsConnected = true;
                Console.WriteLine("Conexión establecida.");
            }
            else
            {
                throw new Exception("No se puedo establecer la conexión con la base de datos en Firebase.");
            }
        }

        public void Write()
        {
            var data = new Entity.Automovil
            {
                Id = "asd123",
                Longitud = "100",
                Latitud = "50"
            };

            SetResponse response = client.Set("Information/"+data.Id, data);

            Entity.Automovil result = response.ResultAs<Entity.Automovil>();

            Console.WriteLine("Informacion insertada.");
        }

        public void Update(string path, object data)
        {
            
            client.Update(path, data);

            //Entity.Automovil result = response.ResultAs<Entity.Automovil>();
        }

        public void ReadFromSemaforos(string path)
        {
            FirebaseResponse response = client.Get(path);

            Dictionary<string, Entity.Interseccion> data = JsonConvert.DeserializeObject<Dictionary<string, Entity.Interseccion>>(response.Body.ToString());

            Console.WriteLine();
        }

        public Dictionary<string, T> ReadOLD<T>(string path)
        {
            FirebaseResponse response = client.Get(path);

            Dictionary<string, T> data = JsonConvert.DeserializeObject<Dictionary<string, T>>(response.Body.ToString());

            return data;
        }

        public T Select<T>(string path)
        {
            FirebaseResponse response = client.Get(path);

            T data = JsonConvert.DeserializeObject<T>(response.Body.ToString());

            return data;
        }

    }

}
