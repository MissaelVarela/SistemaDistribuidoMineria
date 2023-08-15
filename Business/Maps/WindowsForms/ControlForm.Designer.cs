namespace Business.Maps.WindowsForms
{
    partial class ControlForm
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
            this.mapControl = new Business.Maps.WindowsForms.MiningMapControl();
            this.SuspendLayout();
            // 
            // mapControl
            // 
            this.mapControl.Location = new System.Drawing.Point(12, 12);
            this.mapControl.Name = "mapControl";
            this.mapControl.Size = new System.Drawing.Size(900, 550);
            this.mapControl.TabIndex = 0;
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 572);
            this.Controls.Add(this.mapControl);
            this.Name = "ControlForm";
            this.Text = "Control Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ControlForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private MiningMapControl mapControl;
    }
}