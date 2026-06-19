namespace GUI
{
    partial class CambiarContraseña
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CambiarContraseña));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.btnMinimizar = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblContraseñaActual = new System.Windows.Forms.Label();
            this.lblUsuarioFCC = new System.Windows.Forms.Label();
            this.txtNuevaContraseña = new System.Windows.Forms.TextBox();
            this.lblContraseñaFCC = new System.Windows.Forms.Label();
            this.btnGuardarContra = new System.Windows.Forms.Button();
            this.pbMostrarClave = new System.Windows.Forms.PictureBox();
            this.linkVolver = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMostrarClave)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.btnCerrar);
            this.panel1.Controls.Add(this.btnMinimizar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(346, 43);
            this.panel1.TabIndex = 7;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Location = new System.Drawing.Point(318, 4);
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
            this.btnMinimizar.Location = new System.Drawing.Point(290, 4);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(25, 35);
            this.btnMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimizar.TabIndex = 3;
            this.btnMinimizar.TabStop = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 42);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(339, 95);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // txtContraseña
            // 
            this.txtContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContraseña.Location = new System.Drawing.Point(65, 246);
            this.txtContraseña.Multiline = true;
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.PasswordChar = '*';
            this.txtContraseña.Size = new System.Drawing.Size(219, 40);
            this.txtContraseña.TabIndex = 12;
            this.txtContraseña.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContraseña_KeyPress);
            // 
            // txtUsuario
            // 
            this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(62, 167);
            this.txtUsuario.Multiline = true;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(219, 40);
            this.txtUsuario.TabIndex = 11;
            this.txtUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUsuario_KeyPress);
            // 
            // lblContraseñaActual
            // 
            this.lblContraseñaActual.AutoSize = true;
            this.lblContraseñaActual.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContraseñaActual.ForeColor = System.Drawing.Color.White;
            this.lblContraseñaActual.Location = new System.Drawing.Point(58, 219);
            this.lblContraseñaActual.Name = "lblContraseñaActual";
            this.lblContraseñaActual.Size = new System.Drawing.Size(208, 24);
            this.lblContraseñaActual.TabIndex = 10;
            this.lblContraseñaActual.Text = "Contraseña actual:";
            // 
            // lblUsuarioFCC
            // 
            this.lblUsuarioFCC.AutoSize = true;
            this.lblUsuarioFCC.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuarioFCC.ForeColor = System.Drawing.Color.White;
            this.lblUsuarioFCC.Location = new System.Drawing.Point(58, 140);
            this.lblUsuarioFCC.Name = "lblUsuarioFCC";
            this.lblUsuarioFCC.Size = new System.Drawing.Size(86, 24);
            this.lblUsuarioFCC.TabIndex = 9;
            this.lblUsuarioFCC.Text = "Usuario:";
            // 
            // txtNuevaContraseña
            // 
            this.txtNuevaContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNuevaContraseña.Location = new System.Drawing.Point(62, 327);
            this.txtNuevaContraseña.Multiline = true;
            this.txtNuevaContraseña.Name = "txtNuevaContraseña";
            this.txtNuevaContraseña.PasswordChar = '*';
            this.txtNuevaContraseña.Size = new System.Drawing.Size(219, 40);
            this.txtNuevaContraseña.TabIndex = 14;
            this.txtNuevaContraseña.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNuevaContraseña_KeyPress);
            // 
            // lblContraseñaFCC
            // 
            this.lblContraseñaFCC.AutoSize = true;
            this.lblContraseñaFCC.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContraseñaFCC.ForeColor = System.Drawing.Color.White;
            this.lblContraseñaFCC.Location = new System.Drawing.Point(58, 300);
            this.lblContraseñaFCC.Name = "lblContraseñaFCC";
            this.lblContraseñaFCC.Size = new System.Drawing.Size(135, 24);
            this.lblContraseñaFCC.TabIndex = 13;
            this.lblContraseñaFCC.Text = "Contraseña:";
            // 
            // btnGuardarContra
            // 
            this.btnGuardarContra.BackColor = System.Drawing.Color.Maroon;
            this.btnGuardarContra.FlatAppearance.BorderSize = 0;
            this.btnGuardarContra.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGuardarContra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarContra.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarContra.ForeColor = System.Drawing.Color.White;
            this.btnGuardarContra.Location = new System.Drawing.Point(62, 386);
            this.btnGuardarContra.Name = "btnGuardarContra";
            this.btnGuardarContra.Size = new System.Drawing.Size(219, 40);
            this.btnGuardarContra.TabIndex = 15;
            this.btnGuardarContra.Text = "Guardar Contraseña";
            this.btnGuardarContra.UseVisualStyleBackColor = false;
            this.btnGuardarContra.Click += new System.EventHandler(this.btnGuardarContra_Click);
            // 
            // pbMostrarClave
            // 
            this.pbMostrarClave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbMostrarClave.Image = global::GUI.Properties.Resources.OjoAbierto;
            this.pbMostrarClave.Location = new System.Drawing.Point(290, 246);
            this.pbMostrarClave.Name = "pbMostrarClave";
            this.pbMostrarClave.Size = new System.Drawing.Size(41, 40);
            this.pbMostrarClave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMostrarClave.TabIndex = 17;
            this.pbMostrarClave.TabStop = false;
            this.pbMostrarClave.Click += new System.EventHandler(this.pbMostrarClave_Click);
            // 
            // linkVolver
            // 
            this.linkVolver.AutoSize = true;
            this.linkVolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkVolver.LinkColor = System.Drawing.Color.MediumTurquoise;
            this.linkVolver.Location = new System.Drawing.Point(144, 444);
            this.linkVolver.Name = "linkVolver";
            this.linkVolver.Size = new System.Drawing.Size(49, 18);
            this.linkVolver.TabIndex = 16;
            this.linkVolver.TabStop = true;
            this.linkVolver.Text = "Volver";
            this.linkVolver.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkVolver_LinkClicked);
            // 
            // CambiarContraseña
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(346, 486);
            this.Controls.Add(this.pbMostrarClave);
            this.Controls.Add(this.linkVolver);
            this.Controls.Add(this.btnGuardarContra);
            this.Controls.Add(this.txtNuevaContraseña);
            this.Controls.Add(this.lblContraseñaFCC);
            this.Controls.Add(this.txtContraseña);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblContraseñaActual);
            this.Controls.Add(this.lblUsuarioFCC);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CambiarContraseña";
            this.Text = "CambiarContraseña";
            this.Load += new System.EventHandler(this.CambiarContraseña_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMostrarClave)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.PictureBox btnMinimizar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContraseñaActual;
        private System.Windows.Forms.Label lblUsuarioFCC;
        private System.Windows.Forms.TextBox txtNuevaContraseña;
        private System.Windows.Forms.Label lblContraseñaFCC;
        private System.Windows.Forms.Button btnGuardarContra;
        private System.Windows.Forms.PictureBox pbMostrarClave;
        private System.Windows.Forms.LinkLabel linkVolver;
    }
}