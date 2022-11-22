namespace SemaforoApp.SemaforoClient
{
    partial class Controlador
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblSalida = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btnGreenLightRequest = new System.Windows.Forms.Button();
            this.numTurnRequest = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numTurnRequest)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSalida
            // 
            this.lblSalida.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSalida.Location = new System.Drawing.Point(0, 0);
            this.lblSalida.Name = "lblSalida";
            this.lblSalida.Size = new System.Drawing.Size(242, 52);
            this.lblSalida.TabIndex = 0;
            this.lblSalida.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btnGreenLightRequest
            // 
            this.btnGreenLightRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreenLightRequest.Location = new System.Drawing.Point(144, 77);
            this.btnGreenLightRequest.Name = "btnGreenLightRequest";
            this.btnGreenLightRequest.Size = new System.Drawing.Size(98, 23);
            this.btnGreenLightRequest.TabIndex = 1;
            this.btnGreenLightRequest.Text = "GreenLightRequest";
            this.btnGreenLightRequest.UseVisualStyleBackColor = true;
            this.btnGreenLightRequest.Click += new System.EventHandler(this.btnGreenLightRequest_Click);
            // 
            // numTurnRequest
            // 
            this.numTurnRequest.Location = new System.Drawing.Point(109, 79);
            this.numTurnRequest.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numTurnRequest.Name = "numTurnRequest";
            this.numTurnRequest.Size = new System.Drawing.Size(29, 20);
            this.numTurnRequest.TabIndex = 2;
            // 
            // Controlador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 101);
            this.Controls.Add(this.numTurnRequest);
            this.Controls.Add(this.btnGreenLightRequest);
            this.Controls.Add(this.lblSalida);
            this.Name = "Controlador";
            this.Text = "Controlador";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Controlador_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numTurnRequest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSalida;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button btnGreenLightRequest;
        private System.Windows.Forms.NumericUpDown numTurnRequest;
    }
}