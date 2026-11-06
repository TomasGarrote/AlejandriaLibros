namespace GUI
{
    partial class formPrestamo
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
            this.lblTituloLibrosPrestamo = new System.Windows.Forms.Label();
            this.dgvCatalogo = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAplicarFiltro = new System.Windows.Forms.Button();
            this.lblCategoriaLibro = new System.Windows.Forms.Label();
            this.lblAutorLibro = new System.Windows.Forms.Label();
            this.lblTituloLibro = new System.Windows.Forms.Label();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.cmbAutor = new System.Windows.Forms.ComboBox();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnVolver = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaProgramada = new System.Windows.Forms.DateTimePicker();
            this.btnConfirmarPrestamo = new System.Windows.Forms.Button();
            this.lblDias = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.cmbDiasProgramados = new System.Windows.Forms.ComboBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatalogo)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Controls.Add(this.lblTituloLibrosPrestamo);
            this.panel2.Controls.Add(this.dgvCatalogo);
            this.panel2.Location = new System.Drawing.Point(31, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(757, 448);
            this.panel2.TabIndex = 14;
            // 
            // lblTituloLibrosPrestamo
            // 
            this.lblTituloLibrosPrestamo.AutoSize = true;
            this.lblTituloLibrosPrestamo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloLibrosPrestamo.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTituloLibrosPrestamo.Location = new System.Drawing.Point(247, 0);
            this.lblTituloLibrosPrestamo.Name = "lblTituloLibrosPrestamo";
            this.lblTituloLibrosPrestamo.Size = new System.Drawing.Size(232, 29);
            this.lblTituloLibrosPrestamo.TabIndex = 38;
            this.lblTituloLibrosPrestamo.Text = "Libros Disponibles";
            // 
            // dgvCatalogo
            // 
            this.dgvCatalogo.AllowUserToAddRows = false;
            this.dgvCatalogo.Location = new System.Drawing.Point(22, 32);
            this.dgvCatalogo.MultiSelect = false;
            this.dgvCatalogo.Name = "dgvCatalogo";
            this.dgvCatalogo.ReadOnly = true;
            this.dgvCatalogo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCatalogo.Size = new System.Drawing.Size(712, 398);
            this.dgvCatalogo.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Maroon;
            this.panel4.Controls.Add(this.btnLimpiar);
            this.panel4.Controls.Add(this.btnAplicarFiltro);
            this.panel4.Controls.Add(this.lblCategoriaLibro);
            this.panel4.Controls.Add(this.lblAutorLibro);
            this.panel4.Controls.Add(this.lblTituloLibro);
            this.panel4.Controls.Add(this.cmbCategoria);
            this.panel4.Controls.Add(this.cmbAutor);
            this.panel4.Controls.Add(this.txtTitulo);
            this.panel4.Location = new System.Drawing.Point(804, 25);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(217, 247);
            this.panel4.TabIndex = 16;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Maroon;
            this.btnLimpiar.BackgroundImage = global::GUI.Properties.Resources.trash_can;
            this.btnLimpiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(152, 192);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(35, 35);
            this.btnLimpiar.TabIndex = 20;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // btnAplicarFiltro
            // 
            this.btnAplicarFiltro.BackColor = System.Drawing.Color.Maroon;
            this.btnAplicarFiltro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicarFiltro.ForeColor = System.Drawing.Color.White;
            this.btnAplicarFiltro.Location = new System.Drawing.Point(27, 192);
            this.btnAplicarFiltro.Name = "btnAplicarFiltro";
            this.btnAplicarFiltro.Size = new System.Drawing.Size(108, 35);
            this.btnAplicarFiltro.TabIndex = 19;
            this.btnAplicarFiltro.Text = "Aplicar";
            this.btnAplicarFiltro.UseVisualStyleBackColor = false;
            this.btnAplicarFiltro.Click += new System.EventHandler(this.btnAplicarFiltro_Click);
            // 
            // lblCategoriaLibro
            // 
            this.lblCategoriaLibro.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.lblCategoriaLibro.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblCategoriaLibro.Location = new System.Drawing.Point(65, 13);
            this.lblCategoriaLibro.Name = "lblCategoriaLibro";
            this.lblCategoriaLibro.Size = new System.Drawing.Size(88, 20);
            this.lblCategoriaLibro.TabIndex = 19;
            this.lblCategoriaLibro.Text = "Categoria";
            // 
            // lblAutorLibro
            // 
            this.lblAutorLibro.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.lblAutorLibro.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblAutorLibro.Location = new System.Drawing.Point(84, 71);
            this.lblAutorLibro.Name = "lblAutorLibro";
            this.lblAutorLibro.Size = new System.Drawing.Size(51, 18);
            this.lblAutorLibro.TabIndex = 18;
            this.lblAutorLibro.Text = "Autor";
            // 
            // lblTituloLibro
            // 
            this.lblTituloLibro.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloLibro.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTituloLibro.Location = new System.Drawing.Point(84, 125);
            this.lblTituloLibro.Name = "lblTituloLibro";
            this.lblTituloLibro.Size = new System.Drawing.Size(51, 18);
            this.lblTituloLibro.TabIndex = 17;
            this.lblTituloLibro.Text = "Titulo";
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(27, 36);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(160, 21);
            this.cmbCategoria.TabIndex = 2;
            // 
            // cmbAutor
            // 
            this.cmbAutor.FormattingEnabled = true;
            this.cmbAutor.Location = new System.Drawing.Point(27, 92);
            this.cmbAutor.Name = "cmbAutor";
            this.cmbAutor.Size = new System.Drawing.Size(160, 21);
            this.cmbAutor.TabIndex = 1;
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new System.Drawing.Point(27, 146);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(160, 20);
            this.txtTitulo.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.btnVolver);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dtpFechaProgramada);
            this.panel1.Controls.Add(this.btnConfirmarPrestamo);
            this.panel1.Controls.Add(this.lblDias);
            this.panel1.Controls.Add(this.lblFecha);
            this.panel1.Controls.Add(this.cmbDiasProgramados);
            this.panel1.Location = new System.Drawing.Point(804, 289);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 184);
            this.panel1.TabIndex = 21;
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.Maroon;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.Location = new System.Drawing.Point(18, 131);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(187, 35);
            this.btnVolver.TabIndex = 23;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(66, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 18);
            this.label4.TabIndex = 22;
            this.label4.Text = "o";
            // 
            // dtpFechaProgramada
            // 
            this.dtpFechaProgramada.Location = new System.Drawing.Point(97, 37);
            this.dtpFechaProgramada.Name = "dtpFechaProgramada";
            this.dtpFechaProgramada.Size = new System.Drawing.Size(108, 20);
            this.dtpFechaProgramada.TabIndex = 21;
            this.dtpFechaProgramada.ValueChanged += new System.EventHandler(this.dtpFechaProgramada_ValueChanged);
            // 
            // btnConfirmarPrestamo
            // 
            this.btnConfirmarPrestamo.BackColor = System.Drawing.Color.Maroon;
            this.btnConfirmarPrestamo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarPrestamo.ForeColor = System.Drawing.Color.White;
            this.btnConfirmarPrestamo.Location = new System.Drawing.Point(18, 78);
            this.btnConfirmarPrestamo.Name = "btnConfirmarPrestamo";
            this.btnConfirmarPrestamo.Size = new System.Drawing.Size(187, 35);
            this.btnConfirmarPrestamo.TabIndex = 19;
            this.btnConfirmarPrestamo.Text = "Confirmar Prestamo";
            this.btnConfirmarPrestamo.UseVisualStyleBackColor = false;
            this.btnConfirmarPrestamo.Click += new System.EventHandler(this.btnConfirmarPrestamo_Click);
            // 
            // lblDias
            // 
            this.lblDias.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.lblDias.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblDias.Location = new System.Drawing.Point(14, 13);
            this.lblDias.Name = "lblDias";
            this.lblDias.Size = new System.Drawing.Size(88, 20);
            this.lblDias.TabIndex = 19;
            this.lblDias.Text = "Dias";
            // 
            // lblFecha
            // 
            this.lblFecha.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.lblFecha.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblFecha.Location = new System.Drawing.Point(118, 13);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(69, 18);
            this.lblFecha.TabIndex = 18;
            this.lblFecha.Text = "Fecha";
            // 
            // cmbDiasProgramados
            // 
            this.cmbDiasProgramados.FormattingEnabled = true;
            this.cmbDiasProgramados.Location = new System.Drawing.Point(18, 36);
            this.cmbDiasProgramados.Name = "cmbDiasProgramados";
            this.cmbDiasProgramados.Size = new System.Drawing.Size(38, 21);
            this.cmbDiasProgramados.TabIndex = 2;
            this.cmbDiasProgramados.SelectedIndexChanged += new System.EventHandler(this.cmbDiasProgramados_SelectedIndexChanged);
            // 
            // formPrestamo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1042, 494);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Name = "formPrestamo";
            this.Text = "formPrestamo";
            this.Load += new System.EventHandler(this.formPrestamo_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatalogo)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTituloLibrosPrestamo;
        private System.Windows.Forms.DataGridView dgvCatalogo;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAplicarFiltro;
        private System.Windows.Forms.Label lblCategoriaLibro;
        private System.Windows.Forms.Label lblAutorLibro;
        private System.Windows.Forms.Label lblTituloLibro;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.ComboBox cmbAutor;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConfirmarPrestamo;
        private System.Windows.Forms.Label lblDias;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.ComboBox cmbDiasProgramados;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaProgramada;
    }
}