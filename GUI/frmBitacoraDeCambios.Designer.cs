namespace GUI
{
    partial class frmBitacoraDeCambios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBitacoraDeCambios));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.btnMaximizar = new System.Windows.Forms.PictureBox();
            this.btnMinimizar = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblBitacora = new System.Windows.Forms.Label();
            this.dgvBitacoraCambios = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnActivarVersion = new System.Windows.Forms.Button();
            this.btnLimpiarB = new System.Windows.Forms.Button();
            this.btnAplicarB = new System.Windows.Forms.Button();
            this.lblApellidoB = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.lblFechaFinalB = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblFechaInicioB = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblNombreB = new System.Windows.Forms.Label();
            this.lblLoginB = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacoraCambios)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.btnCerrar);
            this.panel1.Controls.Add(this.btnMaximizar);
            this.panel1.Controls.Add(this.btnMinimizar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(966, 41);
            this.panel1.TabIndex = 33;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Location = new System.Drawing.Point(938, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 35);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCerrar.TabIndex = 8;
            this.btnCerrar.TabStop = false;
            // 
            // btnMaximizar
            // 
            this.btnMaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaximizar.Image = ((System.Drawing.Image)(resources.GetObject("btnMaximizar.Image")));
            this.btnMaximizar.Location = new System.Drawing.Point(907, 3);
            this.btnMaximizar.Name = "btnMaximizar";
            this.btnMaximizar.Size = new System.Drawing.Size(25, 35);
            this.btnMaximizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMaximizar.TabIndex = 7;
            this.btnMaximizar.TabStop = false;
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimizar.Image")));
            this.btnMinimizar.Location = new System.Drawing.Point(876, 3);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(25, 35);
            this.btnMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimizar.TabIndex = 6;
            this.btnMinimizar.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Controls.Add(this.lblBitacora);
            this.panel2.Controls.Add(this.dgvBitacoraCambios);
            this.panel2.Location = new System.Drawing.Point(30, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(886, 348);
            this.panel2.TabIndex = 34;
            // 
            // lblBitacora
            // 
            this.lblBitacora.AutoSize = true;
            this.lblBitacora.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBitacora.ForeColor = System.Drawing.SystemColors.Control;
            this.lblBitacora.Location = new System.Drawing.Point(381, 10);
            this.lblBitacora.Name = "lblBitacora";
            this.lblBitacora.Size = new System.Drawing.Size(130, 33);
            this.lblBitacora.TabIndex = 21;
            this.lblBitacora.Text = "Bitacora";
            // 
            // dgvBitacoraCambios
            // 
            this.dgvBitacoraCambios.AllowUserToAddRows = false;
            this.dgvBitacoraCambios.Location = new System.Drawing.Point(18, 46);
            this.dgvBitacoraCambios.MultiSelect = false;
            this.dgvBitacoraCambios.Name = "dgvBitacoraCambios";
            this.dgvBitacoraCambios.ReadOnly = true;
            this.dgvBitacoraCambios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBitacoraCambios.Size = new System.Drawing.Size(850, 284);
            this.dgvBitacoraCambios.TabIndex = 10;
            this.dgvBitacoraCambios.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvBitacoraCambios_CellFormatting);
            this.dgvBitacoraCambios.SelectionChanged += new System.EventHandler(this.dgvBitacoraCambios_SelectionChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Maroon;
            this.panel3.Controls.Add(this.txtDNI);
            this.panel3.Controls.Add(this.txtNombre);
            this.panel3.Controls.Add(this.btnVolver);
            this.panel3.Controls.Add(this.btnActivarVersion);
            this.panel3.Controls.Add(this.btnLimpiarB);
            this.panel3.Controls.Add(this.btnAplicarB);
            this.panel3.Controls.Add(this.lblApellidoB);
            this.panel3.Controls.Add(this.dtpHasta);
            this.panel3.Controls.Add(this.lblFechaFinalB);
            this.panel3.Controls.Add(this.dtpDesde);
            this.panel3.Controls.Add(this.lblFechaInicioB);
            this.panel3.Controls.Add(this.txtApellido);
            this.panel3.Controls.Add(this.lblNombreB);
            this.panel3.Controls.Add(this.lblLoginB);
            this.panel3.Location = new System.Drawing.Point(30, 438);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(886, 150);
            this.panel3.TabIndex = 35;
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(99, 22);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(100, 20);
            this.txtDNI.TabIndex = 24;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(277, 22);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 20);
            this.txtNombre.TabIndex = 23;
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.Maroon;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.Location = new System.Drawing.Point(766, 102);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(108, 35);
            this.btnVolver.TabIndex = 22;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnActivarVersion
            // 
            this.btnActivarVersion.BackColor = System.Drawing.Color.Maroon;
            this.btnActivarVersion.Enabled = false;
            this.btnActivarVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivarVersion.ForeColor = System.Drawing.Color.White;
            this.btnActivarVersion.Location = new System.Drawing.Point(604, 17);
            this.btnActivarVersion.Name = "btnActivarVersion";
            this.btnActivarVersion.Size = new System.Drawing.Size(270, 35);
            this.btnActivarVersion.TabIndex = 21;
            this.btnActivarVersion.Text = "Activar Version";
            this.btnActivarVersion.UseVisualStyleBackColor = false;
            this.btnActivarVersion.Click += new System.EventHandler(this.btnActivarVersion_Click);
            // 
            // btnLimpiarB
            // 
            this.btnLimpiarB.Location = new System.Drawing.Point(438, 104);
            this.btnLimpiarB.Name = "btnLimpiarB";
            this.btnLimpiarB.Size = new System.Drawing.Size(120, 23);
            this.btnLimpiarB.TabIndex = 18;
            this.btnLimpiarB.Text = "Limpiar";
            this.btnLimpiarB.UseVisualStyleBackColor = true;
            this.btnLimpiarB.Click += new System.EventHandler(this.btnLimpiarB_Click);
            // 
            // btnAplicarB
            // 
            this.btnAplicarB.Location = new System.Drawing.Point(438, 66);
            this.btnAplicarB.Name = "btnAplicarB";
            this.btnAplicarB.Size = new System.Drawing.Size(121, 23);
            this.btnAplicarB.TabIndex = 17;
            this.btnAplicarB.Text = "Aplicar";
            this.btnAplicarB.UseVisualStyleBackColor = true;
            this.btnAplicarB.Click += new System.EventHandler(this.btnAplicarB_Click);
            // 
            // lblApellidoB
            // 
            this.lblApellidoB.AutoSize = true;
            this.lblApellidoB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApellidoB.ForeColor = System.Drawing.SystemColors.Control;
            this.lblApellidoB.Location = new System.Drawing.Point(384, 23);
            this.lblApellidoB.Name = "lblApellidoB";
            this.lblApellidoB.Size = new System.Drawing.Size(69, 16);
            this.lblApellidoB.TabIndex = 2;
            this.lblApellidoB.Text = "Apellido:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Location = new System.Drawing.Point(153, 107);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(200, 20);
            this.dtpHasta.TabIndex = 16;
            // 
            // lblFechaFinalB
            // 
            this.lblFechaFinalB.AutoSize = true;
            this.lblFechaFinalB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaFinalB.ForeColor = System.Drawing.SystemColors.Control;
            this.lblFechaFinalB.Location = new System.Drawing.Point(56, 111);
            this.lblFechaFinalB.Name = "lblFechaFinalB";
            this.lblFechaFinalB.Size = new System.Drawing.Size(87, 16);
            this.lblFechaFinalB.TabIndex = 6;
            this.lblFechaFinalB.Text = "Fecha final:";
            // 
            // dtpDesde
            // 
            this.dtpDesde.Location = new System.Drawing.Point(153, 69);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(200, 20);
            this.dtpDesde.TabIndex = 15;
            // 
            // lblFechaInicioB
            // 
            this.lblFechaInicioB.AutoSize = true;
            this.lblFechaInicioB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaInicioB.ForeColor = System.Drawing.SystemColors.Control;
            this.lblFechaInicioB.Location = new System.Drawing.Point(56, 72);
            this.lblFechaInicioB.Name = "lblFechaInicioB";
            this.lblFechaInicioB.Size = new System.Drawing.Size(95, 16);
            this.lblFechaInicioB.TabIndex = 5;
            this.lblFechaInicioB.Text = "Fecha inicio:";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(459, 22);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtApellido.Size = new System.Drawing.Size(100, 20);
            this.txtApellido.TabIndex = 10;
            // 
            // lblNombreB
            // 
            this.lblNombreB.AutoSize = true;
            this.lblNombreB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreB.ForeColor = System.Drawing.SystemColors.Control;
            this.lblNombreB.Location = new System.Drawing.Point(205, 23);
            this.lblNombreB.Name = "lblNombreB";
            this.lblNombreB.Size = new System.Drawing.Size(66, 16);
            this.lblNombreB.TabIndex = 1;
            this.lblNombreB.Text = "Nombre:";
            // 
            // lblLoginB
            // 
            this.lblLoginB.AutoSize = true;
            this.lblLoginB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginB.ForeColor = System.Drawing.SystemColors.Control;
            this.lblLoginB.Location = new System.Drawing.Point(56, 26);
            this.lblLoginB.Name = "lblLoginB";
            this.lblLoginB.Size = new System.Drawing.Size(37, 16);
            this.lblLoginB.TabIndex = 3;
            this.lblLoginB.Text = "DNI:";
            // 
            // frmBitacoraDeCambios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(966, 618);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmBitacoraDeCambios";
            this.Text = "frmBitacoraDeCambios";
            this.Load += new System.EventHandler(this.frmBitacoraDeCambios_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacoraCambios)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.PictureBox btnMaximizar;
        private System.Windows.Forms.PictureBox btnMinimizar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvBitacoraCambios;
        private System.Windows.Forms.Label lblBitacora;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnLimpiarB;
        private System.Windows.Forms.Button btnAplicarB;
        private System.Windows.Forms.Label lblApellidoB;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label lblFechaFinalB;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblFechaInicioB;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblNombreB;
        private System.Windows.Forms.Label lblLoginB;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnActivarVersion;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.TextBox txtNombre;
    }
}