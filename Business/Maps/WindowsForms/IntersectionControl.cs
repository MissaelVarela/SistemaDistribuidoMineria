using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Maps.MapObjects;
using System.Reflection;

namespace Business.Maps.WindowsForms
{
    public partial class IntersectionControl : UserControl
    {
        private MapObjects.Intersection semaforo;

        private readonly Button[,] buttons;
        private readonly Label[,] labels;

        public IntersectionControl()
        {
            InitializeComponent();
            buttons = new Button[,] { { btn0, btn1,      btn2 }, 
                                      { btn3, btnCentro, btn4 }, 
                                      { btn5, btn6,      btn7 }  };
            
            labels = new Label[,] { { lblText0, lblText1, lblText2 },
                                    { lblText3, null,     lblText4 },
                                    { lblText5, lblText6, lblText7 }  };

            /* btn0.BackgroundImage = Resources.SemaforoImages.GrisGrande;
             btn1.BackgroundImage = Resources.SemaforoImages.GrisGrande;
             btn2.BackgroundImage = Resources.SemaforoImages.GrisGrande;
             btn3.BackgroundImage = Resources.SemaforoImages.GrisGrande;
             btn4.BackgroundImage = Resources.SemaforoImages.GrisGrande;
             btn5.BackgroundImage = Resources.SemaforoImages.GrisGrande;
             btn6.BackgroundImage = Resources.SemaforoImages.GrisGrande;
             btn7.BackgroundImage = Resources.SemaforoImages.GrisGrande;*/

            this.BorderStyle = BorderStyle.Fixed3D;

            // Quitar Selectable a botones:
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    MethodInfo method = buttons[i, j].GetType().GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (method != null)
                        method.Invoke(buttons[i, j], new object[] { ControlStyles.Selectable, false });
                }
            }

            //UpdateVisibles();
        }

        public void AssignIntersection(MapObjects.Intersection semaforo)
        {
            this.semaforo = semaforo;

            UpdateVisibles();

            for (int i = 0, c = 0; i < semaforo.DireccionesActivas.Length; i++)
            {
                if(semaforo.DireccionesActivas[i])
                {
                    Intersection.ConvertDirection(i.ToString(), out int j, out int k);
                    buttons[j, k].Text = c.ToString();
                    c++;
                }
            }
        }

        public void RemoveIntersection()
        {
            semaforo = null;

            UpdateVisibles();
        }

        public bool IsThisAssigned(MapObjects.Intersection semaforo)
        {
            return this.semaforo == semaforo;
        }

        public void UpdateImages()
        {
            int len = buttons.GetLength(0);

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    Intersection.Estado estado = semaforo.DirectionState(i, j);

                    if (estado != Intersection.Estado.Nulo)
                    {
                        buttons[i, j].BackgroundImage = Resources.SemaforoImages.GetSemaforoGrande(estado);
                    }
                    
                }
            }
        }

        private void UpdateVisibles()
        {
            int len = buttons.GetLength(0);
            bool semaforoAssiged = semaforo != null;

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    Intersection.Estado estado;

                    if (semaforoAssiged)
                        estado = semaforo.DirectionState(i, j);
                    else
                        estado = Intersection.Estado.Nulo;

                    if (estado != Intersection.Estado.Nulo)
                    {
                        buttons[i, j].Visible = true;
                        buttons[i, j].BackgroundImage = Resources.SemaforoImages.GetSemaforoGrande(estado);

                        if (labels[i, j] != null)
                            labels[i, j].Visible = true;
                    }
                    else
                    {
                        buttons[i, j].Visible = false;

                        if(labels[i, j] != null) 
                            labels[i, j].Visible = false;
                    }
                }
            }

            if (btnCentro.BackgroundImage == null)
                btnCentro.BackgroundImage = Resources.SemaforoImages.CentroGrande;

            if (semaforoAssiged)
                btnCentro.Visible = true;
            else
                btnCentro.Visible = false;
        }

        public void AddButtonEventHandler(int i, int j, System.EventHandler eventHandler)
        {
            buttons[i, j].Click += eventHandler;
        }

        public void AddButtonEventHandlers(System.EventHandler eventHandler)
        {
            btn0.Click += eventHandler;
            btn1.Click += eventHandler;
            btn2.Click += eventHandler;
            btn3.Click += eventHandler;
            btn4.Click += eventHandler;
            btn5.Click += eventHandler;
            btn6.Click += eventHandler;
            btn7.Click += eventHandler;
        }

        private void btn0_Click(object sender, EventArgs e)
        {

        }
    }
}
