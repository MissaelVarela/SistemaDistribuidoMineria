using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SemaforoApp.SemaforoClient
{
    public partial class Controlador : Form
    {
        private int seg = 0, min = 0;

        private SemaforoDisplay[] forms;
        public Controlador(SemaforoDisplay[] forms)
        {
            InitializeComponent();

            timer.Interval = 1000;

            this.forms = forms;

            this.StartPosition = FormStartPosition.CenterScreen;

            numTurnRequest.Maximum = forms.Length - 1;

            StartForms();

            this.Select();
        }

        private void StartForms()
        {
            if (forms != null || forms.Length != 0)
            {
                for (int i = 0; i < forms.Length; i++)
                {
                    forms[i].Location = new Point(100 + i * 180, 100);
                    forms[i].Show();
                }
            }

            if (forms != null || forms.Length != 0)
            {
                for (int i = 0; i < forms.Length; i++)
                {
                    forms[i].Start();
                    //forms[i].TESTGreenLightRequest(3);
                }
            }

            timer.Start();
        }

        private void Controlador_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(forms != null || forms.Length != 0)
            {
                for (int i = 0; i < forms.Length; i++)
                {
                    forms[i].Close();
                }
            }

            timer.Stop();

            Application.Exit();
        }

        private void btnGreenLightRequest_Click(object sender, EventArgs e)
        {
            byte turn = byte.Parse(numTurnRequest.Value.ToString());

            for (int i = 0; i < forms.Length; i++)
            {
                forms[i].TESTGreenLightRequest(turn);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            seg++;
            if(seg == 60)
            {
                min++;
                seg = 0;
            }

            string salida = $"{min,2}:{seg,2}";

            lblSalida.Text = salida;
        }
    }
}
