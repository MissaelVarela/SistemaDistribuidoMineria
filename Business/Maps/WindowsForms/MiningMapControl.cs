using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

using Business.Maps.MapObjects;
using Business.Maps.Resources;

namespace Business.Maps.WindowsForms
{
    public partial class MiningMapControl : UserControl
    {
        private GMapOverlay markerOverlaySemaforos;

        private const double Latitud = 27.103612;
        private const double Longitud = -109.415555;

        private readonly List<IntersectionAndMarker> intersectionAndMarkers = new List<IntersectionAndMarker>();

        private bool wasDrag;
        private bool clickDown;

        public string SelectedId 
        {
            get;
            private set;
        }

        public MiningMapControl()
        {
            InitializeComponent();
            InitializeMap();

           // IntersectionControl.Visible = false;
        }

        // Metodos

        public void AddCongestionesButtonsHandler(EventHandler reportHandler, EventHandler deleteHandler)
        {
            this.btnReportCongestion.Click += reportHandler;
            this.btnDeleteCongestion.Click += deleteHandler;
        }

        private void InitializeMap()
        {
            gMapControl.Position = new PointLatLng(Latitud, Longitud);

            gMapControl.DragButton = MouseButtons.Left;

            gMapControl.MapProvider = GMapProviders.GoogleMap;

            gMapControl.MinZoom = 15;
            gMapControl.MaxZoom = 19;
            gMapControl.Zoom = 18;

            gMapControl.BorderStyle = BorderStyle.FixedSingle;
            //gMapControl.BoundsOfMap = new RectLatLng(point, size);

            gMapControl.CanDragMap = true;
            gMapControl.AutoScroll = true;

            markerOverlaySemaforos = new GMapOverlay("Semaforos");
            gMapControl.Overlays.Add(markerOverlaySemaforos);
        }

        public void AddIntersection(Intersection intersection)
        {
            GMapMarker semaforoMarker = CreateMarkerTest(intersection);

            intersectionAndMarkers.Add(new IntersectionAndMarker(intersection, semaforoMarker));

            markerOverlaySemaforos.Markers.Add(semaforoMarker);
        }

        private GMapMarker CreateMarkerTest(Intersection semaforo)
        {
            PointLatLng coords = new PointLatLng(semaforo.Latitud, semaforo.Longitud);

            Image image = SemaforoImages.Base;

            // Actualizar graficamente todas las direcciones
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Intersection.Estado state = semaforo.DirectionState(i, j);

                    if (state != Intersection.Estado.Nulo)
                    {
                        image = SemaforoImages.UpdateSemaforo(image, i, j, state);
                    }
                }
            }

            GMapMarker semaforoMarker = new GMarkerGoogle(coords, (Bitmap)image);
            
            return semaforoMarker;
        }

        private GMapMarker CreateMarker(Intersection semaforo)
        {
            PointLatLng coords = new PointLatLng(semaforo.Latitud, semaforo.Longitud);

            GMarkerGoogleType type = GMarkerGoogleType.gray_small;

            // Asignar imagen correspondiente al marker.
            if(semaforo.DirectionState(0, 1) == Intersection.Estado.Rojo)
            {
                type = GMarkerGoogleType.red;
            }
            else if(semaforo.DirectionState(0, 1) == Intersection.Estado.Verde)
            {
                type = GMarkerGoogleType.green;
            }

            GMapMarker semaforoMarker = new GMarkerGoogle(coords, type);

            return semaforoMarker;
        }

        public void UpdateIntersection(Intersection newVersion)
        {
            int i = 0;
            foreach (var item in intersectionAndMarkers)
            {
                if (item.interseccion.Id == newVersion.Id)
                {
                    // Actualizar valores
                    item.interseccion.UpdateEstados(newVersion.Estados);

                    // Crear nuevo marker apartir de los nuevos valores
                    GMapMarker semaforoMarker = CreateMarkerTest(item.interseccion);

                    // Remplazar el marker graficamente
                    markerOverlaySemaforos.Markers.Remove(item.marker);
                    markerOverlaySemaforos.Markers.Add(semaforoMarker);

                    intersectionAndMarkers[i].marker = semaforoMarker;

                    // Actualiza las imagenes de los botones tambien
                    IntersectionControl.UpdateImages();

                    return;
                }

                i++;
            }
        }

        public void UpdateSemaforo(string id, string intersectionId, byte value)
        {
            int c = 0;
            foreach (var item in intersectionAndMarkers)
            {
                if (item.interseccion.Id.Equals(intersectionId))
                {
                    // Actualizo el estado
                    Intersection.ConvertDirection(id, out int i, out int j);
                    item.interseccion.Estados[i, j] = (Intersection.Estado)value;

                    // Crear nuevo marker apartir de los nuevos valores
                    GMapMarker semaforoMarker = CreateMarkerTest(item.interseccion);

                    // Remplazar el marker graficamente
                    markerOverlaySemaforos.Markers.Remove(item.marker);
                    markerOverlaySemaforos.Markers.Add(semaforoMarker);

                    intersectionAndMarkers[c].marker = semaforoMarker;

                    // Actualiza las imagenes de los botones tambien
                    IntersectionControl.UpdateImages();

                    return;
                }
                c++;
            }
        }

        public void LoadMap()
        {
            foreach (var item in intersectionAndMarkers)
            {
                //item.interseccion.UpdateEstados();
            }
        }

        private void ChangeState()
        {

        }

        private void SelectIntersection(Intersection intersection)
        {
            IntersectionControl.AssignIntersection(intersection);

            SelectedId = intersection.Id;

            lblTittle.Text = "ID de la Intersección: " + intersection.Id;
            lblTittle.Visible = true;
        }

        private void UnselectIntersection()
        {
            IntersectionControl.RemoveIntersection();

            lblTittle.Text = "";
            lblTittle.Visible = false;
        }

        public void AddButtonEventHandlers(System.EventHandler eventHandler)
        {
            IntersectionControl.AddButtonEventHandlers(eventHandler);
        }

        public Intersection.Estado GetEstado(string id, string direccion)
        {
            foreach (var item in intersectionAndMarkers)
            {
                if (item.interseccion.Id.Equals(id))
                {
                    int i; int j;
                    Intersection.ConvertDirection(direccion, out i, out j);
                    Intersection.Estado estado = item.interseccion.DirectionState(i, j);

                    return estado;
                }
            }

            throw new Exception("No se encontró la interseccion por id para obtener su estado.");
        }

        // Eventos


        private void gMapControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (!wasDrag)
            {
                bool isMouseOver = false;

                foreach (var item in intersectionAndMarkers)
                {
                    if (item.marker.IsMouseOver)
                    {
                        if (!IntersectionControl.IsThisAssigned(item.interseccion))
                        {
                            SelectIntersection(item.interseccion);
                        }

                        isMouseOver = true;
                        break;
                    }
                }

                if (!isMouseOver)
                {
                    UnselectIntersection();
                }
            }

            wasDrag = false;
            clickDown = false;
            
        }

        private void gMapControl_MouseDown(object sender, MouseEventArgs e)
        {
            clickDown = true;
        }

        private void gMapControl_MouseMove(object sender, MouseEventArgs e)
        {
            if(clickDown)
                wasDrag = true;
        }
    }
}
