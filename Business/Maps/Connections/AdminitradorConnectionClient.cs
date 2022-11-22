using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.Api;
using Newtonsoft.Json;

namespace Business.Maps.Connections
{
    class AdminitradorConnectionClient
    {
        private WebSocketClient WebSocket { get; set; }

        public delegate void DataChangedEventHandler(string id, string intersectionId, byte state);
        public event DataChangedEventHandler DataChanged;

        private bool connected;

        public AdminitradorConnectionClient()
        {
            WebSocket = new WebSocketClient();
            WebSocket.Path = "wss://administradorsemaforos.herokuapp.com/";
            WebSocket.Timeout = 180;
            WebSocket.MessageReceived += MessageReceived;

            WebSocket.Create();
        }

        public async Task Connect()
        {
            if(!connected)
            {
                await WebSocket.Connect();

                connected = true;
            }
            
        }

        public void SendLogin(string pass)
        {
            if(connected)
            {
                Entity.Message loginRequest = new Entity.Message()
                {
                    Type = "controlAppLogin",
                    Content = pass
                };

                WebSocket.Send(loginRequest);
            }
        }

        public void SendGreenLightRequest(string intersectionId, string turnSelected)
        {
            if (connected)
            {
                Entity.Request.GreenLightRequestContent content = new Entity.Request.GreenLightRequestContent()
                {
                    IntersectionId = intersectionId,
                    GreenLightTurn = turnSelected
                };

                Entity.Message greenLightRequest = new Entity.Message()
                {
                    Type = "greenLightRequest",
                    Content = content
                };

                WebSocket.Send(greenLightRequest);
            }
        }

        private void MessageReceived(string message)
        {
            try
            {
                Entity.Message response = JsonConvert.DeserializeObject<Entity.Message>(message);

                if(response.Type.Equals("dataChanged")) // dataChanged
                {
                    Entity.Response.DataChangedContent content = 
                        JsonConvert.DeserializeObject<Entity.Response.DataChangedContent>(response.Content.ToString());

                    Console.WriteLine(content);

                    DataChanged(content.Id, content.IntersectionId, content.State);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("No se puedo deserializar el objeto que llegó. Llegó: "+ message);
                Console.WriteLine(e.Message);
            }
        }

        public void Close()
        {
            WebSocket.Close();
        }
    }
}
