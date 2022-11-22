using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Business.Maps.WindowsForms
{
    public partial class ControlForm : Form
    {
        private readonly Information.SistemaMineriaInformation info;
        private readonly Controllers.MapSemaforosController controller;
        private readonly Connections.AdminitradorConnectionClient connection;

        public ControlForm()
        {
            InitializeComponent();

            info = new Information.SistemaMineriaInformation();
            controller = new Controllers.MapSemaforosController();
            connection = new Connections.AdminitradorConnectionClient();

            connection.DataChanged += Connection_DataChanged;

            StartPosition = FormStartPosition.CenterScreen;

            LoadMap();
            LoadEventHandlers();

            ConnectToAdminitradorApp();
        }

        private async void ConnectToAdminitradorApp()
        {
            try
            {
                await connection.Connect();

                connection.SendLogin("12345678");
            }
            catch(Exception e)
            {
                Console.WriteLine("Algo falló en la conexion. \n" + e.Message);
                connection.Close();
            }
        }

        private void LoadMap()
        {
            // Se obtiene la informacion de los Semaforos en la BD.
            List<MapObjects.Intersection> semaforos = info.GetIntersections();

            foreach (var semaforo in semaforos)
            {
                mapControl.AddIntersection(semaforo);
            }
        }

        private void LoadEventHandlers()
        {
            EventHandler eventHandler = new EventHandler(SemaforoButton_Click);
            mapControl.AddButtonEventHandlers(eventHandler);

            mapControl.AddCongestionesButtonsHandler(btnReportCongestion_Click, btnDeleteCongestion_Click);
        }

        public void UpdateIntersection(string id)
        {
            MapObjects.Intersection intersection = info.GetIntersection(id);

            mapControl.UpdateIntersection(intersection);
        }

        public void UpdateMap()
        {
            
        }

        private void SemaforoButton_Click(object sender, EventArgs e)
        {
            // Datos 

            Button button = (Button)sender;

            string intersectionId = mapControl.SelectedId;
            string turnSelected = button.Text;

            /*Console.WriteLine($"Interseccion {intersectionId} y la direccion es {direccion}");
            this.Text = $"Interseccion {intersectionId} y la direccion es {direccion}";*/

            // Confirmación

            string message = $"Intersección: {intersectionId}, Semaforo: {turnSelected} \n" +
                $"¿Confirmas la de solicitud?";

            if(MessageBox.Show(message, "Confirmación", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // MessageBox.Show("Peticion enviada");
                connection.SendGreenLightRequest(intersectionId, turnSelected);
                return;
            }
            else
                return;

            // Procesar

            

            // Detectar solicitud

            // Enviar solicitud

            // ----------------------------

            // TEST... Simulacion del comportamiento
           /* MapObjects.Intersection.Estado estado = mapControl.GetEstado(intersectionId, semaforoId);
            if (estado != MapObjects.Intersection.Estado.Verde)
            {
                info.TurnGreenSemaforo(intersectionId, semaforoId);
            }
            else 
            {
                info.TurnRedSemaforo(intersectionId, semaforoId);
            }

            UpdateIntersection(intersectionId);*/

        }

        private void Connection_DataChanged(string id, string intersectionId, byte state)
        {
            mapControl.UpdateSemaforo(id, intersectionId, state);
        }

        private void ControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

        private void btnReportCongestion_Click(object sender, EventArgs e)
        {
            info.UpdateCongestion("1");
        }

        private void btnDeleteCongestion_Click(object sender, EventArgs e)
        {
            info.UpdateCongestion("0");
        }
    }
}
