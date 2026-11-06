namespace GUI
{
    partial class formDevolucion
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTituloPrestamosPendientes = new System.Windows.Forms.Label();
            this.dgvPrestamosPendientes = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAplicarFiltro = new System.Windows.Forms.Button();
            this.lblDNICliente = new System.Windows.Forms.Label();
            this.txtDniCliente = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnConfirmarDevolucion = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrestamosPendientes)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Controls.Add(this.lblTituloPrestamosPendientes);
            this.panel2.Controls.Add(this.dgvPrestamosPendientes);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(757, 448);
            this.panel2.TabIndex = 15;
            // 
            // lblTituloPrestamosPendientes
            // 
            this.lblTituloPrestamosPendientes.AutoSize = true;
            this.lblTituloPrestamosPendientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloPrestamosPendientes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTituloPrestamosPendientes.Location = new System.Drawing.Point(247, 0);
            this.lblTituloPrestamosPendientes.Name = "lblTituloPrestamosPendientes";
            this.lblTituloPrestamosPendientes.Size = new System.Drawing.Size(227, 29);
            this.lblTituloPrestamosPendientes.TabIndex = 38;
            this.lblTituloPrestamosPendientes.Text = "Prestamos Activos";
            // 
            // dgvPrestamosPendientes
            // 
            this.dgvPrestamosPendientes.AllowUserToAddRows = false;
            this.dgvPrestamosPendientes.Location = new System.Drawing.Point(22, 32);
            this.dgvPrestamosPendientes.MultiSelect = false;
            this.dgvPrestamosPendientes.Name = "dgvPrestamosPendientes";
            this.dgvPrestamosPendientes.ReadOnly = true;
            this.dgvPrestamosPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPrestamosPendientes.Size = new System.Drawing.Size(712, 398);
            this.dgvPrestamosPendientes.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Maroon;
            this.panel4.Controls.Add(this.lblTitulo);
            this.panel4.Controls.Add(this.txtTitulo);
            this.panel4.Controls.Add(this.btnLimpiar);
            this.panel4.Controls.Add(this.btnAplicarFiltro);
            this.panel4.Controls.Add(this.lblDNICliente);
            this.panel4.Controls.Add(this.txtDniCliente);
            this.panel4.Location = new System.Drawing.Point(779, 12);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(217, 198);
            this.panel4.TabIndex = 17;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTitulo.Location = new System.Drawing.Point(89, 21);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(59, 18);
            this.lblTitulo.TabIndex = 22;
            this.lblTitulo.Text = "Titulo";
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new System.Drawing.Point(29, 42);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(160, 20);
            this.txtTitulo.TabIndex = 21;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Maroon;
            this.btnLimpiar.BackgroundImage = global::GUI.Properties.Resources.trash_can;
            this.btnLimpiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(154, 142);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(35, 35);
            this.btnLimpiar.TabIndex = 20;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAplicarFiltro
            // 
            this.btnAplicarFiltro.BackColor = System.Drawing.Color.Maroon;
            this.btnAplicarFiltro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicarFiltro.ForeColor = System.Drawing.Color.White;
            this.btnAplicarFiltro.Location = new System.Drawing.Point(29, 142);
            this.btnAplicarFiltro.Name = "btnAplicarFiltro";
            this.btnAplicarFiltro.Size = new System.Drawing.Size(108, 35);
            this.btnAplicarFiltro.TabIndex = 19;
            this.btnAplicarFiltro.Text = "Aplicar";
            this.btnAplicarFiltro.UseVisualStyleBackColor = false;
            this.btnAplicarFiltro.Click += new System.EventHandler(this.btnAplicarFiltro_Click);
            // 
            // lblDNICliente
            // 
            this.lblDNICliente.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.lblDNICliente.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblDNICliente.Location = new System.Drawing.Point(67, 80);
            this.lblDNICliente.Name = "lblDNICliente";
            this.lblDNICliente.Size = new System.Drawing.Size(103, 18);
            this.lblDNICliente.TabIndex = 17;
            this.lblDNICliente.Text = "DNI Cliente";
            // 
            // txtDniCliente
            // 
            this.txtDniCliente.Location = new System.Drawing.Point(29, 101);
            this.txtDniCliente.Name = "txtDniCliente";
            this.txtDniCliente.Size = new System.Drawing.Size(160, 20);
            this.txtDniCliente.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.btnVolver);
            this.panel1.Controls.Add(this.btnConfirmarDevolucion);
            this.panel1.Location = new System.Drawing.Point(779, 229);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 126);
            this.panel1.TabIndex = 23;
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.Maroon;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.Location = new System.Drawing.Point(18, 72);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(187, 35);
            this.btnVolver.TabIndex = 23;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnConfirmarDevolucion
            // 
            this.btnConfirmarDevolucion.BackColor = System.Drawing.Color.Maroon;
            this.btnConfirmarDevolucion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarDevolucion.ForeColor = System.Drawing.Color.White;
            this.btnConfirmarDevolucion.Location = new System.Drawing.Point(18, 19);
            this.btnConfirmarDevolucion.Name = "btnConfirmarDevolucion";
            this.btnConfirmarDevolucion.Size = new System.Drawing.Size(187, 35);
            this.btnConfirmarDevolucion.TabIndex = 19;
            this.btnConfirmarDevolucion.Text = "Confirmar Devolucion";
            this.btnConfirmarDevolucion.UseVisualStyleBackColor = false;
            this.btnConfirmarDevolucion.Click += new System.EventHandler(this.btnDevolucion_Click);
            // 
            // formDevolucion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1008, 556);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Name = "formDevolucion";
            this.Text = "formDevolucion";
            this.Load += new System.EventHandler(this.formDevolucion_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrestamosPendientes)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTituloPrestamosPendientes;
        private System.Windows.Forms.DataGridView dgvPrestamosPendientes;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAplicarFiltro;
        private System.Windows.Forms.Label lblDNICliente;
        private System.Windows.Forms.TextBox txtDniCliente;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnConfirmarDevolucion;
    }
}