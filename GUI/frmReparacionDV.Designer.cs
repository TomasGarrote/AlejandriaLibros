namespace GUI
{
    partial class frmReparacionDV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReparacionDV));
            this.lstInconsistencias = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblErrorDV = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.btnMinimizar = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.lstBackups = new System.Windows.Forms.ListBox();
            this.btnRecalcularDV = new System.Windows.Forms.Button();
            this.lblModuloReparacion = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstInconsistencias
            // 
            this.lstInconsistencias.FormattingEnabled = true;
            this.lstInconsistencias.Location = new System.Drawing.Point(16, 38);
            this.lstInconsistencias.Name = "lstInconsistencias";
            this.lstInconsistencias.Size = new System.Drawing.Size(668, 95);
            this.lstInconsistencias.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.lblErrorDV);
            this.panel1.Controls.Add(this.lstInconsistencias);
            this.panel1.Location = new System.Drawing.Point(58, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 148);
            this.panel1.TabIndex = 2;
            // 
            // lblErrorDV
            // 
            this.lblErrorDV.AutoSize = true;
            this.lblErrorDV.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorDV.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblErrorDV.Location = new System.Drawing.Point(216, 7);
            this.lblErrorDV.Name = "lblErrorDV";
            this.lblErrorDV.Size = new System.Drawing.Size(262, 24);
            this.lblErrorDV.TabIndex = 2;
            this.lblErrorDV.Text = "Inconsistencias Detectadas";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Controls.Add(this.btnCerrar);
            this.panel2.Controls.Add(this.btnMinimizar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(816, 43);
            this.panel2.TabIndex = 8;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Location = new System.Drawing.Point(788, 4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 35);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCerrar.TabIndex = 5;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimizar.Image")));
            this.btnMinimizar.Location = new System.Drawing.Point(760, 4);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(25, 35);
            this.btnMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimizar.TabIndex = 3;
            this.btnMinimizar.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Maroon;
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.lblModuloReparacion);
            this.panel3.Location = new System.Drawing.Point(58, 239);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(700, 366);
            this.panel3.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnRestaurar);
            this.groupBox1.Controls.Add(this.lstBackups);
            this.groupBox1.Controls.Add(this.btnRecalcularDV);
            this.groupBox1.Location = new System.Drawing.Point(29, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(641, 293);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(48, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(251, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Forzar nuevos digitos verificadores";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(48, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Seleccione una version:";
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Location = new System.Drawing.Point(348, 159);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(254, 42);
            this.btnRestaurar.TabIndex = 4;
            this.btnRestaurar.Text = "Restaurar Version Anterior";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // lstBackups
            // 
            this.lstBackups.FormattingEnabled = true;
            this.lstBackups.Location = new System.Drawing.Point(51, 159);
            this.lstBackups.Name = "lstBackups";
            this.lstBackups.Size = new System.Drawing.Size(281, 108);
            this.lstBackups.TabIndex = 3;
            // 
            // btnRecalcularDV
            // 
            this.btnRecalcularDV.Location = new System.Drawing.Point(51, 56);
            this.btnRecalcularDV.Name = "btnRecalcularDV";
            this.btnRecalcularDV.Size = new System.Drawing.Size(551, 44);
            this.btnRecalcularDV.TabIndex = 0;
            this.btnRecalcularDV.Text = "Recalcular Digito Verificadores";
            this.btnRecalcularDV.UseVisualStyleBackColor = true;
            this.btnRecalcularDV.Click += new System.EventHandler(this.btnRecalcularDV_Click);
            // 
            // lblModuloReparacion
            // 
            this.lblModuloReparacion.AutoSize = true;
            this.lblModuloReparacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModuloReparacion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblModuloReparacion.Location = new System.Drawing.Point(237, 10);
            this.lblModuloReparacion.Name = "lblModuloReparacion";
            this.lblModuloReparacion.Size = new System.Drawing.Size(216, 24);
            this.lblModuloReparacion.TabIndex = 3;
            this.lblModuloReparacion.Text = "Modulo de reparacion";
            // 
            // frmReparacionDV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(816, 631);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmReparacionDV";
            this.Text = "   ";
            this.Load += new System.EventHandler(this.frmReparacionDV_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstInconsistencias;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblErrorDV;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.PictureBox btnMinimizar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstBackups;
        private System.Windows.Forms.Button btnRecalcularDV;
        private System.Windows.Forms.Label lblModuloReparacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.Label label2;
    }
}