using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SemaforoApp.SemaforoClient
{
    public class SemaforoDisplay : Form
    {
        private SemaforoApp semaforo;

        private Label[] labels;

        private Button btnConnect;

        private bool started = false;

        public SemaforoDisplay(SemaforoApp semaforo)
        {
            InitializeComponent();

            this.semaforo = semaforo;

            System.Drawing.Size size = new System.Drawing.Size(150, 400);
            MaximumSize = size;
            MinimumSize = size;
            Size = size;

            Text = "";

            int centrado = Size.Width / 2 - 40 - 8;

            Label lblVerde = new Label
            {
                Size = new System.Drawing.Size(80, 80),
                Location = new System.Drawing.Point(centrado, 240),
                BackColor = GetColor(2, false),
                BorderStyle = BorderStyle.FixedSingle,
                Image = GetImage(2, false)
            };
            Label lblAmarillo = new Label
            {
                Size = new System.Drawing.Size(80, 80),
                Location = new System.Drawing.Point(centrado, 140),
                BackColor = GetColor(1, false),
                BorderStyle = BorderStyle.FixedSingle,
                Image = GetImage(1, false)
            };
            Label lblRojo = new Label
            {
                Size = new System.Drawing.Size(80, 80),
                Location = new System.Drawing.Point(centrado, 40),
                BackColor = GetColor(0, false),
                BorderStyle = BorderStyle.FixedSingle,
                Image = GetImage(0, false)
            };
            Label lblText = new Label
            {
                Size = new System.Drawing.Size(20, 20),
                Location = new System.Drawing.Point(Width - 40, 10),
                Text = SemaforoApp.GetDirection(semaforo.Id),
                Font = new Font("Imprint MT Shadow", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)))
            };
            btnConnect = new Button()
            {
                Size = new System.Drawing.Size(40, 20),
                Location = new System.Drawing.Point(Width - 60, Height - 60),
                Text = "Connect",
                Font = new Font("Imprint MT Shadow", 6F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                FlatStyle = FlatStyle.Flat
            };
            btnConnect.Click += btnConnect_Click;

            Controls.Add(lblVerde);
            Controls.Add(lblAmarillo);
            Controls.Add(lblRojo);
            Controls.Add(lblText);
            Controls.Add(btnConnect);

            labels = new Label[] { lblVerde, lblAmarillo, lblRojo };

            // Subscribiendo el evento
            semaforo.StateChanged += Semaforo_StateChanged;
            this.FormClosing += Form_Closing;

            this.BackColor = System.Drawing.Color.FromArgb(247, 154, 36);


            this.Text = semaforo.Id;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Select();
        }

        public void Start()
        {
            if (!started)
            {
                Thread threadSemaforo = new Thread(new ThreadStart(semaforo.Start));
                semaforo.RunningThread = threadSemaforo;

                threadSemaforo.Start();

                started = true;
            }
        }

        public void TESTGreenLightRequest(byte turn)
        {
            semaforo.GreenLight(turn);
        }

        private void ChangeColor(int luz)
        {
            for (int i = 0; i < labels.Length; i++)
            {
                labels[i].BackColor = GetColor(i, false);
                labels[i].Image = GetImage(i, false);
            }

            labels[luz].BackColor = GetColor(luz, true);
            labels[luz].Image = GetImage(luz, true);
        }

        private System.Drawing.Color GetColor(int luz, bool on)
        {
            switch (luz)
            {
                case 0:
                    if (on)
                        return System.Drawing.Color.LawnGreen;
                    else
                        return System.Drawing.Color.Gray;

                case 1:
                    if (on)
                        return System.Drawing.Color.Yellow;
                    else
                        return System.Drawing.Color.Gray;

                case 2:
                    if (on)
                        return System.Drawing.Color.Red;
                    else
                        return System.Drawing.Color.Gray;

                default:
                    return System.Drawing.Color.DarkGray;
            }
        }

        private Image GetImage(int luz, bool on)
        {
            string path = "..\\..\\Resources\\";

            switch (luz)
            {
                case 0:
                    if (on)
                        return Image.FromFile(path + "verde.png"); 
                    else
                        return Image.FromFile(path + "gris.png");

                case 1:
                    if (on)
                        return Image.FromFile(path + "amarillo.png");
                    else
                        return Image.FromFile(path + "gris.png");

                case 2:
                    if (on)
                        return Image.FromFile(path + "rojo.png");
                    else
                        return Image.FromFile(path + "gris.png");

                default:
                    return Image.FromFile(path + "gris.png");
            }
        }


        private void Semaforo_StateChanged(byte state)
        {
            //Console.WriteLine("Display cacho evento: State -> " + state);

            switch (state)
            {
                case 2:
                    ChangeColor(0);
                    break;
                case 3:
                    ChangeColor(1);
                    break;
                case 4:
                    ChangeColor(2);
                    break;
            }
        }

        private void Form_Closing(object sender, EventArgs e)
        {
            semaforo.Stop();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            semaforo.Connect();

            btnConnect.BackColor = Color.AliceBlue;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SemaforoDisplay
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "SemaforoDisplay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SemaforoDisplay_FormClosing);
            this.ResumeLayout(false);

        }

        private void SemaforoDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            semaforo.Close();
        }
    }
}
