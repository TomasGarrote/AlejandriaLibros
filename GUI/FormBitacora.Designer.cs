namespace GUI
{
    partial class FormBitacora
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBitacora));
            this.dtgvBitacora = new System.Windows.Forms.DataGridView();
            this.lblNombreB = new System.Windows.Forms.Label();
            this.lblApellidoB = new System.Windows.Forms.Label();
            this.lblLoginB = new System.Windows.Forms.Label();
            this.lblModuloB = new System.Windows.Forms.Label();
            this.lblFechaInicioB = new System.Windows.Forms.Label();
            this.lblFechaFinalB = new System.Windows.Forms.Label();
            this.lblEventoB = new System.Windows.Forms.Label();
            this.lblCriticidadB = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.cmbLogin = new System.Windows.Forms.ComboBox();
            this.cmbModulo = new System.Windows.Forms.ComboBox();
            this.cmbEvento = new System.Windows.Forms.ComboBox();
            this.cmbCriticidad = new System.Windows.Forms.ComboBox();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.btnMaximizar = new System.Windows.Forms.PictureBox();
            this.btnMinimizar = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnImprimirB = new System.Windows.Forms.Button();
            this.btnLimpiarB = new System.Windows.Forms.Button();
            this.btnAplicarB = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblBitacora = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvBitacora)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgvBitacora
            // 
            this.dtgvBitacora.BackgroundColor = System.Drawing.SystemColors.WindowFrame;
            this.dtgvBitacora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvBitacora.Location = new System.Drawing.Point(5, 38);
            this.dtgvBitacora.Name = "dtgvBitacora";
            this.dtgvBitacora.Size = new System.Drawing.Size(876, 238);
            this.dtgvBitacora.TabIndex = 0;
            // 
            // lblNombreB
            // 
            this.lblNombreB.AutoSize = true;
            this.lblNombreB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreB.ForeColor = System.Drawing.SystemColors.Control;
            this.lblNombreB.Location = new System.Drawing.Point(42, 24);
            this.lblNombreB.Name = "lblNombreB";
            this.lblNombreB.Size = new System.Drawing.Size(66, 16);
            this.lblNombreB.TabIndex = 1;
            this.lblNombreB.Text = "Nombre:";
            // 
            // lblApellidoB
            // 
            this.lblApellidoB.AutoSize = true;
            this.lblApellidoB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApellidoB.ForeColor = System.Drawing.SystemColors.Control;
            this.lblApellidoB.Location = new System.Drawing.Point(264, 27);
            this.lblApellidoB.Name = "lblApellidoB";
            this.lblApellidoB.Size = new System.Drawing.Size(69, 16);
            this.lblApellidoB.TabIndex = 2;
            this.lblApellidoB.Text = "Apellido:";
            // 
            // lblLoginB
            // 
            this.lblLoginB.AutoSize = true;
            this.lblLoginB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginB.ForeColor = System.Drawing.SystemColors.Control;
            this.lblLoginB.Location = new System.Drawing.Point(59, 68);
            this.lblLoginB.Name = "lblLoginB";
            this.lblLoginB.Size = new System.Drawing.Size(49, 16);
            this.lblLoginB.TabIndex = 3;
            this.lblLoginB.Text = "Login:";
            // 
            // lblModuloB
            // 
            this.lblModuloB.AutoSize = true;
            this.lblModuloB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModuloB.ForeColor = System.Drawing.SystemColors.Control;
            this.lblModuloB.Location = new System.Drawing.Point(46, 96);
            this.lblModuloB.Name = "lblModuloB";
            this.lblModuloB.Size = new System.Drawing.Size(62, 16);
            this.lblModuloB.TabIndex = 4;
            this.lblModuloB.Text = "Modulo:";
            // 
            // lblFechaInicioB
            // 
            this.lblFechaInicioB.AutoSize = true;
            this.lblFechaInicioB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaInicioB.ForeColor = System.Drawing.SystemColors.Control;
            this.lblFechaInicioB.Location = new System.Drawing.Point(238, 69);
            this.lblFechaInicioB.Name = "lblFechaInicioB";
            this.lblFechaInicioB.Size = new System.Drawing.Size(95, 16);
            this.lblFechaInicioB.TabIndex = 5;
            this.lblFechaInicioB.Text = "Fecha inicio:";
            // 
            // lblFechaFinalB
            // 
            this.lblFechaFinalB.AutoSize = true;
            this.lblFechaFinalB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaFinalB.ForeColor = System.Drawing.SystemColors.Control;
            this.lblFechaFinalB.Location = new System.Drawing.Point(541, 68);
            this.lblFechaFinalB.Name = "lblFechaFinalB";
            this.lblFechaFinalB.Size = new System.Drawing.Size(87, 16);
            this.lblFechaFinalB.TabIndex = 6;
            this.lblFechaFinalB.Text = "Fecha final:";
            // 
            // lblEventoB
            // 
            this.lblEventoB.AutoSize = true;
            this.lblEventoB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEventoB.ForeColor = System.Drawing.SystemColors.Control;
            this.lblEventoB.Location = new System.Drawing.Point(274, 96);
            this.lblEventoB.Name = "lblEventoB";
            this.lblEventoB.Size = new System.Drawing.Size(59, 16);
            this.lblEventoB.TabIndex = 7;
            this.lblEventoB.Text = "Evento:";
            // 
            // lblCriticidadB
            // 
            this.lblCriticidadB.AutoSize = true;
            this.lblCriticidadB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriticidadB.ForeColor = System.Drawing.SystemColors.Control;
            this.lblCriticidadB.Location = new System.Drawing.Point(551, 96);
            this.lblCriticidadB.Name = "lblCriticidadB";
            this.lblCriticidadB.Size = new System.Drawing.Size(77, 16);
            this.lblCriticidadB.TabIndex = 8;
            this.lblCriticidadB.Text = "Criticidad:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(114, 23);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(100, 20);
            this.txtNombre.TabIndex = 9;
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(335, 23);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtApellido.Size = new System.Drawing.Size(100, 20);
            this.txtApellido.TabIndex = 10;
            // 
            // cmbLogin
            // 
            this.cmbLogin.FormattingEnabled = true;
            this.cmbLogin.Location = new System.Drawing.Point(114, 68);
            this.cmbLogin.Name = "cmbLogin";
            this.cmbLogin.Size = new System.Drawing.Size(121, 21);
            this.cmbLogin.TabIndex = 11;
            // 
            // cmbModulo
            // 
            this.cmbModulo.FormattingEnabled = true;
            this.cmbModulo.Location = new System.Drawing.Point(114, 95);
            this.cmbModulo.Name = "cmbModulo";
            this.cmbModulo.Size = new System.Drawing.Size(121, 21);
            this.cmbModulo.TabIndex = 12;
            // 
            // cmbEvento
            // 
            this.cmbEvento.FormattingEnabled = true;
            this.cmbEvento.Location = new System.Drawing.Point(335, 95);
            this.cmbEvento.Name = "cmbEvento";
            this.cmbEvento.Size = new System.Drawing.Size(121, 21);
            this.cmbEvento.TabIndex = 13;
            // 
            // cmbCriticidad
            // 
            this.cmbCriticidad.FormattingEnabled = true;
            this.cmbCriticidad.Location = new System.Drawing.Point(634, 95);
            this.cmbCriticidad.Name = "cmbCriticidad";
            this.cmbCriticidad.Size = new System.Drawing.Size(121, 21);
            this.cmbCriticidad.TabIndex = 14;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Location = new System.Drawing.Point(335, 66);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaInicio.TabIndex = 15;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Location = new System.Drawing.Point(634, 65);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaFinal.TabIndex = 16;
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
            this.panel1.Size = new System.Drawing.Size(1121, 43);
            this.panel1.TabIndex = 17;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Location = new System.Drawing.Point(1092, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 35);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCerrar.TabIndex = 5;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnMaximizar
            // 
            this.btnMaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaximizar.Image = ((System.Drawing.Image)(resources.GetObject("btnMaximizar.Image")));
            this.btnMaximizar.Location = new System.Drawing.Point(1061, 3);
            this.btnMaximizar.Name = "btnMaximizar";
            this.btnMaximizar.Size = new System.Drawing.Size(25, 35);
            this.btnMaximizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMaximizar.TabIndex = 4;
            this.btnMaximizar.TabStop = false;
            this.btnMaximizar.Click += new System.EventHandler(this.btnMaximizar_Click);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimizar.Image")));
            this.btnMinimizar.Location = new System.Drawing.Point(1030, 3);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(25, 35);
            this.btnMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimizar.TabIndex = 3;
            this.btnMinimizar.TabStop = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Controls.Add(this.btnImprimirB);
            this.panel2.Controls.Add(this.btnLimpiarB);
            this.panel2.Controls.Add(this.btnAplicarB);
            this.panel2.Controls.Add(this.lblApellidoB);
            this.panel2.Controls.Add(this.lblEventoB);
            this.panel2.Controls.Add(this.lblCriticidadB);
            this.panel2.Controls.Add(this.dtpFechaFinal);
            this.panel2.Controls.Add(this.lblFechaFinalB);
            this.panel2.Controls.Add(this.dtpFechaInicio);
            this.panel2.Controls.Add(this.lblFechaInicioB);
            this.panel2.Controls.Add(this.txtNombre);
            this.panel2.Controls.Add(this.txtApellido);
            this.panel2.Controls.Add(this.cmbCriticidad);
            this.panel2.Controls.Add(this.lblModuloB);
            this.panel2.Controls.Add(this.lblNombreB);
            this.panel2.Controls.Add(this.cmbLogin);
            this.panel2.Controls.Add(this.cmbEvento);
            this.panel2.Controls.Add(this.lblLoginB);
            this.panel2.Controls.Add(this.cmbModulo);
            this.panel2.Location = new System.Drawing.Point(120, 417);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(886, 168);
            this.panel2.TabIndex = 18;
            // 
            // btnImprimirB
            // 
            this.btnImprimirB.Location = new System.Drawing.Point(634, 121);
            this.btnImprimirB.Name = "btnImprimirB";
            this.btnImprimirB.Size = new System.Drawing.Size(121, 23);
            this.btnImprimirB.TabIndex = 19;
            this.btnImprimirB.Text = "Imprimir";
            this.btnImprimirB.UseVisualStyleBackColor = true;
            this.btnImprimirB.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnLimpiarB
            // 
            this.btnLimpiarB.Location = new System.Drawing.Point(114, 121);
            this.btnLimpiarB.Name = "btnLimpiarB";
            this.btnLimpiarB.Size = new System.Drawing.Size(120, 23);
            this.btnLimpiarB.TabIndex = 18;
            this.btnLimpiarB.Text = "Limpiar";
            this.btnLimpiarB.UseVisualStyleBackColor = true;
            this.btnLimpiarB.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAplicarB
            // 
            this.btnAplicarB.Location = new System.Drawing.Point(335, 122);
            this.btnAplicarB.Name = "btnAplicarB";
            this.btnAplicarB.Size = new System.Drawing.Size(121, 23);
            this.btnAplicarB.TabIndex = 17;
            this.btnAplicarB.Text = "Aplicar";
            this.btnAplicarB.UseVisualStyleBackColor = true;
            this.btnAplicarB.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(209, 103);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // lblBitacora
            // 
            this.lblBitacora.AutoSize = true;
            this.lblBitacora.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBitacora.ForeColor = System.Drawing.SystemColors.Control;
            this.lblBitacora.Location = new System.Drawing.Point(375, 3);
            this.lblBitacora.Name = "lblBitacora";
            this.lblBitacora.Size = new System.Drawing.Size(130, 33);
            this.lblBitacora.TabIndex = 20;
            this.lblBitacora.Text = "Bitacora";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Maroon;
            this.panel3.Controls.Add(this.dtgvBitacora);
            this.panel3.Controls.Add(this.lblBitacora);
            this.panel3.Location = new System.Drawing.Point(120, 118);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(886, 281);
            this.panel3.TabIndex = 21;
            // 
            // FormBitacora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1121, 619);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormBitacora";
            this.Text = "Bitacora";
            this.Load += new System.EventHandler(this.FormBitacora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvBitacora)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgvBitacora;
        private System.Windows.Forms.Label lblNombreB;
        private System.Windows.Forms.Label lblApellidoB;
        private System.Windows.Forms.Label lblLoginB;
        private System.Windows.Forms.Label lblModuloB;
        private System.Windows.Forms.Label lblFechaInicioB;
        private System.Windows.Forms.Label lblFechaFinalB;
        private System.Windows.Forms.Label lblEventoB;
        private System.Windows.Forms.Label lblCriticidadB;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.ComboBox cmbLogin;
        private System.Windows.Forms.ComboBox cmbModulo;
        private System.Windows.Forms.ComboBox cmbEvento;
        private System.Windows.Forms.ComboBox cmbCriticidad;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.PictureBox btnMaximizar;
        private System.Windows.Forms.PictureBox btnMinimizar;
        private System.Windows.Forms.Label lblBitacora;
        private System.Windows.Forms.Button btnAplicarB;
        private System.Windows.Forms.Button btnImprimirB;
        private System.Windows.Forms.Button btnLimpiarB;
        private System.Windows.Forms.Panel panel3;
    }
}