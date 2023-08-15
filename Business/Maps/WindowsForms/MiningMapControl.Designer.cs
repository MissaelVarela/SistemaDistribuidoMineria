namespace Business.Maps.WindowsForms
{
    partial class MiningMapControl
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panControl = new System.Windows.Forms.Panel();
            this.grpCongestions = new System.Windows.Forms.GroupBox();
            this.btnDeleteCongestion = new System.Windows.Forms.Button();
            this.btnReportCongestion = new System.Windows.Forms.Button();
            this.grpCotrol = new System.Windows.Forms.GroupBox();
            this.IntersectionControl = new Business.Maps.WindowsForms.IntersectionControl();
            this.lblTittle = new System.Windows.Forms.Label();
            this.gMapControl = new GMap.NET.WindowsForms.GMapControl();
            this.panControl.SuspendLayout();
            this.grpCongestions.SuspendLayout();
            this.grpCotrol.SuspendLayout();
            this.SuspendLayout();
            // 
            // panControl
            // 
            this.panControl.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panControl.Controls.Add(this.grpCongestions);
            this.panControl.Controls.Add(this.grpCotrol);
            this.panControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.panControl.Location = new System.Drawing.Point(550, 0);
            this.panControl.MaximumSize = new System.Drawing.Size(350, 0);
            this.panControl.Name = "panControl";
            this.panControl.Size = new System.Drawing.Size(350, 550);
            this.panControl.TabIndex = 1;
            // 
            // grpCongestions
            // 
            this.grpCongestions.Controls.Add(this.btnDeleteCongestion);
            this.grpCongestions.Controls.Add(this.btnReportCongestion);
            this.grpCongestions.Location = new System.Drawing.Point(20, 377);
            this.grpCongestions.Name = "grpCongestions";
            this.grpCongestions.Size = new System.Drawing.Size(310, 100);
            this.grpCongestions.TabIndex = 3;
            this.grpCongestions.TabStop = false;
            this.grpCongestions.Text = "Congestiones";
            // 
            // btnDeleteCongestion
            // 
            this.btnDeleteCongestion.Location = new System.Drawing.Point(9, 48);
            this.btnDeleteCongestion.Name = "btnDeleteCongestion";
            this.btnDeleteCongestion.Size = new System.Drawing.Size(140, 23);
            this.btnDeleteCongestion.TabIndex = 1;
            this.btnDeleteCongestion.Text = "Quitar congestión";
            this.btnDeleteCongestion.UseVisualStyleBackColor = true;
            // 
            // btnReportCongestion
            // 
            this.btnReportCongestion.Location = new System.Drawing.Point(9, 19);
            this.btnReportCongestion.Name = "btnReportCongestion";
            this.btnReportCongestion.Size = new System.Drawing.Size(140, 23);
            this.btnReportCongestion.TabIndex = 0;
            this.btnReportCongestion.Text = "Reportar congestión";
            this.btnReportCongestion.UseVisualStyleBackColor = true;
            // 
            // grpCotrol
            // 
            this.grpCotrol.Controls.Add(this.IntersectionControl);
            this.grpCotrol.Controls.Add(this.lblTittle);
            this.grpCotrol.Location = new System.Drawing.Point(20, 17);
            this.grpCotrol.Name = "grpCotrol";
            this.grpCotrol.Size = new System.Drawing.Size(310, 345);
            this.grpCotrol.TabIndex = 2;
            this.grpCotrol.TabStop = false;
            this.grpCotrol.Text = "Control de Intersección";
            // 
            // IntersectionControl
            // 
            this.IntersectionControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(244)))));
            this.IntersectionControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IntersectionControl.Location = new System.Drawing.Point(5, 40);
            this.IntersectionControl.Name = "IntersectionControl";
            this.IntersectionControl.Size = new System.Drawing.Size(300, 300);
            this.IntersectionControl.TabIndex = 0;
            // 
            // lblTittle
            // 
            this.lblTittle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTittle.Location = new System.Drawing.Point(6, 16);
            this.lblTittle.Name = "lblTittle";
            this.lblTittle.Size = new System.Drawing.Size(284, 20);
            this.lblTittle.TabIndex = 1;
            this.lblTittle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // gMapControl
            // 
            this.gMapControl.Bearing = 0F;
            this.gMapControl.CanDragMap = true;
            this.gMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl.GrayScaleMode = false;
            this.gMapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl.LevelsKeepInMemmory = 5;
            this.gMapControl.Location = new System.Drawing.Point(0, 0);
            this.gMapControl.MarkersEnabled = true;
            this.gMapControl.MaxZoom = 2;
            this.gMapControl.MinZoom = 2;
            this.gMapControl.MouseWheelZoomEnabled = true;
            this.gMapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl.Name = "gMapControl";
            this.gMapControl.NegativeMode = false;
            this.gMapControl.PolygonsEnabled = true;
            this.gMapControl.RetryLoadTile = 0;
            this.gMapControl.RoutesEnabled = true;
            this.gMapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl.ShowTileGridLines = false;
            this.gMapControl.Size = new System.Drawing.Size(550, 550);
            this.gMapControl.TabIndex = 2;
            this.gMapControl.Zoom = 0D;
            this.gMapControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gMapControl_MouseClick);
            this.gMapControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gMapControl_MouseDown);
            this.gMapControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gMapControl_MouseMove);
            // 
            // MiningMapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gMapControl);
            this.Controls.Add(this.panControl);
            this.Name = "MiningMapControl";
            this.Size = new System.Drawing.Size(900, 550);
            this.panControl.ResumeLayout(false);
            this.grpCongestions.ResumeLayout(false);
            this.grpCotrol.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panControl;
        private GMap.NET.WindowsForms.GMapControl gMapControl;
        private IntersectionControl IntersectionControl;
        private System.Windows.Forms.Label lblTittle;
        private System.Windows.Forms.GroupBox grpCongestions;
        private System.Windows.Forms.Button btnReportCongestion;
        private System.Windows.Forms.GroupBox grpCotrol;
        private System.Windows.Forms.Button btnDeleteCongestion;
    }
}
