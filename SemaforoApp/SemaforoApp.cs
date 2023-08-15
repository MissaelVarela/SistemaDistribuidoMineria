using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaforoApp
{
    public class SemaforoApp
    {
        // Constantes
        private const int GREEN_TIME = 5000;
        private const int YELLOW_TIME = 2000;

        private readonly int RED_TIME;

        // Tiempo de extra de compesacion que sucede antes de ponerse en verde.
        private const int OFFSET_TIME = 500;

        private const int EXTRA_TIME = 0;

        // Propiedades y campos
        public string IntersectionId { get; set; }
        public string Id { get; set; }
        public string Clave { get; set; } 

        public Thread RunningThread { get; set; }

        // Campos
        private readonly Communication.Api.WebSocketClient WebSocket;
        private bool connected;
        

        public byte State
        {
            get => state;

            private set 
            {
                state = value;

                //Console.WriteLine("Semaforo genero evento: Estado update -> " + state);

                // Generar evento
                StateChanged(value);

                // Enviar a la conexión; Si es que existe.
                SendStateChanged(value);
            } 
        }
        private byte state;

        private byte cycle;
        private byte myTurn;

        private bool running;

        // Eventos
        public event StateChangedEventHandler StateChanged;
        public delegate void StateChangedEventHandler(byte state);

        /// <summary>
        /// Contructor de la clase SemaforoApp
        /// </summary>
        /// <param name="cycle">Cantidad de elementos que forman parte del circuito. Se usa para calcular los tiempo de un semaforo.</param>
        /// <param name="turn">Número para identificar el turno del semaforo dentro de un ciclo. De base 0.</param>
        public SemaforoApp(byte cycle, byte turn)
        {
            if (turn < 0 || turn >= cycle)
                throw new Exception("El turno especificado no debe ser menor a 0 ni mayor al tamaño del ciclo.");

            state = 1;
            this.cycle = cycle;
            this.myTurn = turn;

            //(C-1)*(V+A) + C*O
            //RED_TIME = ((cycle - 1) * (GREEN_TIME + YELLOW_TIME) + cycle * OFFSET_TIME) + OFFSET_TIME;
            RED_TIME = (cycle - 1) * (GREEN_TIME + YELLOW_TIME + OFFSET_TIME) + YELLOW_TIME + OFFSET_TIME;


            // Creación del socket de conexión.
            WebSocket = new Communication.Api.WebSocketClient("wss://administradorsemaforos.herokuapp.com/");
            WebSocket.Timeout = (RED_TIME / 1000) + 200;
            WebSocket.Create();
            WebSocket.MessageReceived += WebSocketClient_MessageReceived;
        }

        // Metodos del semaforo
        public async void Start()
        {
            try
            {
                if(!running)
                {
                    running = true;

                    // Todos lo semaforos inician en Rojo a menos que seas el turno inicial.
                    if (myTurn != 0)
                    {
                        Console.WriteLine($"Sem{Id} no es mi turno.");
                        state = 4;
                        //Thread.Sleep(myTurn * 20);
                    }
                    else
                        Console.WriteLine($"Sem{Id} sí es mi turno.");

                    // Se espera el tiempo necesario para empezar correctamente en su turno
                    await WaitToStart(myTurn);

                    while (running)
                    {
                        UpdateState();
                        await Wait(State);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Excepcion generada en el metodo Start del SemafroApp.\n" + e.Message);
            }
            
        }

        public void Stop()
        {
            try
            {
                running = false;

                if (RunningThread != null)
                {
                    RunningThread.Abort();
                    RunningThread = null;
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Falló algo al detener un semaforo.");
            }
            
        }

        private void UpdateState()
        {
            switch (State)
            {
                case 1:
                case 2:
                case 3:
                    State++;
                    break;
                case 4:
                    State = 2;
                    break;
            }

            Console.WriteLine($"Sem{Id} actualiza estado a {State}.");
        }

        private void UpdateState(byte state)
        {

        }

        public async void GreenLight(byte greenLightTurn)
        {
            if (!(greenLightTurn >= 0 && greenLightTurn <= cycle)) 
                throw new Exception("Se intento un GreenLight especificando un turn no valido. Turno envido: "+greenLightTurn.ToString());

            // Primero se detener el semaforo
            Stop();

            // Calcular mi nuevo turno

            for (int i = 0, j = greenLightTurn; i < cycle; i++)
            {
                if (j == myTurn)
                {
                    myTurn = byte.Parse(i.ToString());
                    break;
                }

                j++;
                if (j >= cycle) j = 0;
            }

            Console.WriteLine($"El nuevo turno del Sem{Id} es {myTurn}.");

            // El semaforo esperara un tiempo antes de que suceda el GreenLight por seguridad
            // Si no me toca empezar y ademas mi estado es Verde: me pondre en amarillo para avisar...
            /*if (myTurn != 0 && State == 2)
            {
                State = 3;
            }*/

            // Reinicio el estado sin generar evento.
            
            state = 1;

           /* if (myTurn != 0)
            {
                if (State == 4)
                    state = 1;

                if (State == 3)
                    state = 1;

                if (State == 2)
                {
                    State = 3;
                    state = 1;
                }
            }
            else
            {
                if (State == 4)
                    state = 1;

                if (State == 3)
                    state = 1;

                if (State == 2)
                    state = 1;
            }*/
            

            // Y todos esperaran el tiempo de un YELLOW_TIME
            //await Wait(3);

            // Crear un nuevo RunningThread
            Thread newRunningThread = new Thread(new ThreadStart(this.Start));
            RunningThread = newRunningThread;

            // Correr el semaforo
            newRunningThread.Start();
        }

        private async Task Wait(byte state)
        {
            
            switch (state)
            {
                case 2:
                    Console.WriteLine($"Sem{Id} espera {GREEN_TIME}s.");
                    await Task.Run(() => Thread.Sleep(GREEN_TIME)); 
                    break;
                case 3:
                    Console.WriteLine($"Sem{Id} espera {YELLOW_TIME}s.");
                    await Task.Run(() => Thread.Sleep(YELLOW_TIME));
                    break;
                case 4:
                    Console.WriteLine($"Sem{Id} espera {RED_TIME}s.");
                    await Task.Run(() => Thread.Sleep(RED_TIME));
                    break;
            }
        }

        private async Task WaitToStart(byte turn)
        {
            int waiting = turn * (GREEN_TIME + YELLOW_TIME + OFFSET_TIME);

            Console.WriteLine($"Sem{Id} espera {waiting}s para empezar.");

            await Task.Run(() => Thread.Sleep(waiting));
        }

        // Metodos de comunicación
        public async void Connect()
        {
            if(!connected)
            {
                connected = true;
                await WebSocket.Connect();

                SendLogin();
            }
        }

        private void SendLogin()
        {
            if (connected)
            {
                Entity.Request.LoginContent content = new Entity.Request.LoginContent()
                {
                    id = this.Id,
                    intersectionId = this.IntersectionId,
                    pass = this.Clave
                };

                Entity.Message loginRequest = new Entity.Message()
                {
                    Type = "login",
                    Content = content
                };

                WebSocket.Send(loginRequest);
            }
        }

        private void SendStateChanged(byte state)
        {
            if(connected)
            {
                string message = state.ToString();

                Entity.Message stateChangedRequest = new Entity.Message()
                {
                    Type = "stateChanged",
                    Content = message
                };

                Console.WriteLine("El semaforo " + Id + " envió: " + message);
                WebSocket.Send(stateChangedRequest);
            }
        }

        // Evento de comunicación
        private void WebSocketClient_MessageReceived(string message)
        {
            try
            {
                Console.WriteLine("Llego mensaje a SemaforoApp: " + message);

                Entity.Message response = WebSocket.DeserializeObject<Entity.Message>(message);

                if (response.Type.Equals("greenLightRequested"))
                {
                    byte turn = (byte)response.Content;

                    GreenLight(turn);
                }

                if (response.Equals("Recconection"))
                {
                    SendLogin();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Falló algo al recibir el mensaje en el SemaforoApp. Llegó: " + message);
            }
        }

        public void Close()
        {
            WebSocket.Close();
        }

        // Extra
        public static string GetDirection(string id)
        {
            switch(id)
            {
                case "0": return "NW";
                case "1": return "N";
                case "2": return "NE";
                case "3": return "W";
                case "4": return "E";
                case "5": return "SW";
                case "6": return "S";
                case "7": return "SE";
                default: return "Error";
            }
        }

        // VERDE = 5s; AMARILLO = 2s; ROJO = (C-1)*(V+A) + C*O = 15.5s

        /* 0, N, 0s; 1, R, 7s + 0.5s; 2, R, 14s + 2*0.5s; 
         * 
         * 0, V, 5s                0s
         * 
         * 0, A, 2s                5s
         * 
         * 0, R, 15.5s;            7s
         * 1, V, 5s                  7.5s
         * 
         * 1, A, 2s                  12.5s
         * 
         * 1, R, 15.5s;              14.5s
         * 2, V, 5s                    15s
         * 
         * 2, A, 2s                    20s
         * 
         * 2, R, 15.5s;                22s
         * 0, V, 5s                22.5s
         * 
         * 0, A, 2s                27.5
         * 
         * 0, R, 15.5s             29.5
         * 1, V, 5s                  30s
         * 
         * 1, A, 2s                  35s
         * 
         * 1, R, 15.s                37s
         * 2, V, 5s                    37.5s
         * 
         * 
         * 
         * */
    }
}
