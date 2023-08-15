using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Websocket.Client;

namespace Communication.Api
{
    public class WebSocketClient
    {
        private string streaming_API_Key = "YOUR_USER_KEY";
        

        private string mensaje { get; set; }

        private async void Connect(string path)
        {
            var cliente = new ClientWebSocket();

            WebSocketReceiveResult result;

            var buffer = new byte[1024 * 4];

            var mensaje = new ArraySegment<byte>(buffer);

            await cliente.ConnectAsync(new Uri(path), CancellationToken.None);

            await Task.Run(async () => {

                while (true)
                {



                    result = await cliente.ReceiveAsync(mensaje, CancellationToken.None);

                    var messageBytes = mensaje.Skip(mensaje.Offset).Take(result.Count).ToArray();

                    string receivedMessage = Encoding.UTF8.GetString(messageBytes);

                }

            });
        }

        // ---------- 2 ----------

        WebsocketClient Client;
        public string Path { get; set; }
        public int Timeout { get; set; } = 60; // segundos

        public delegate void MessageReceivedEventHandler(string message);
        public event MessageReceivedEventHandler MessageReceived;

        /*public delegate void MessageReceivedEventHandler(string message);
        public event MessageReceivedEventHandler MessageReceived;*/

        public WebSocketClient() { }

        public WebSocketClient(string path) 
        {
            Path = path;
        }

        public void Create()
        {
            var url = new Uri(Path);
            Client = new WebsocketClient(url);

            Client.ReconnectTimeout = TimeSpan.FromSeconds(Timeout);

            Client.ReconnectionHappened.Subscribe(info =>
            {
                Console.WriteLine("Reconnection happened, type: " + info.Type);

                MessageReceived("Recconection");
            });

            Client.MessageReceived.Subscribe(msg =>
            {
                Console.WriteLine("Message received: " + msg);

                MessageReceived(msg.Text);
            });
        }

        public async Task Connect()
        {
            try
            {
                await Client.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());

                Close();

                Client.Dispose();
            }
        }


        public void Send(string message)
        {
            try
            {
                Task.Run(() => Client.Send(message));
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());

                Close();

                Client.Dispose();
            }
            
        }

        public void Send(object json)
        {
            string message = JsonConvert.SerializeObject(json);
            Send(message);
        }

        /*public void MessageReceived(ResponseMessage message)
        { 

        }

        public async Task InitAsync(string path)
        {
            try
            {
                var exitEvent = new ManualResetEvent(false);
                var url = new Uri(path);

                var client = new WebsocketClient(url);

                Client = client;

                client.ReconnectTimeout = TimeSpan.FromSeconds(30);
                client.ReconnectionHappened.Subscribe(info =>
                {
                    Console.WriteLine("Reconnection happened, type: " + info.Type);
                });
                client.MessageReceived.Subscribe(msg =>
                {
                    Console.WriteLine("Message received: " + msg);
                    if (msg.ToString().ToLower() == "connected")
                    {
                        string data = "{\"userKey\":\"" + streaming_API_Key + "\", \"symbol\":\"EURUSD,GBPUSD,USDJPY\"}";
                        client.Send(data);
                    }
                });

                await client.Start();

                // exitEvent.WaitOne();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());

                Close();
            }
        }*/

        public void Close()
        { 
            if (Client != null)
            {
                Client.Stop(WebSocketCloseStatus.NormalClosure, "Desconexión");
                Client.Dispose();
            } 
        }

        public T DeserializeObject<T>(string value)
        {
            T obj = JsonConvert.DeserializeObject<T>(value);
            return obj;
        }

    }


}
