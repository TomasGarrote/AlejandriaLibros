namespace GUI
{
    partial class formCrearCliente
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
            this.panModificarUsuario = new System.Windows.Forms.Panel();
            this.btnCrear = new System.Windows.Forms.Button();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblApe = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.txtApe = new System.Windows.Forms.TextBox();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.panModificarUsuario.SuspendLayout();
            this.SuspendLayout();
            // 
            // panModificarUsuario
            // 
            this.panModificarUsuario.AllowDrop = true;
            this.panModificarUsuario.BackColor = System.Drawing.Color.Maroon;
            this.panModificarUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panModificarUsuario.Controls.Add(this.btnVolver);
            this.panModificarUsuario.Controls.Add(this.btnCrear);
            this.panModificarUsuario.Controls.Add(this.lblTelefono);
            this.panModificarUsuario.Controls.Add(this.txtTelefono);
            this.panModificarUsuario.Controls.Add(this.lblEmail);
            this.panModificarUsuario.Controls.Add(this.lblNombre);
            this.panModificarUsuario.Controls.Add(this.lblApe);
            this.panModificarUsuario.Controls.Add(this.label2);
            this.panModificarUsuario.Controls.Add(this.txtEmail);
            this.panModificarUsuario.Controls.Add(this.txtNom);
            this.panModificarUsuario.Controls.Add(this.txtApe);
            this.panModificarUsuario.Controls.Add(this.txtDni);
            this.panModificarUsuario.Location = new System.Drawing.Point(12, 12);
            this.panModificarUsuario.Name = "panModificarUsuario";
            this.panModificarUsuario.Size = new System.Drawing.Size(284, 248);
            this.panModificarUsuario.TabIndex = 54;
            // 
            // btnCrear
            // 
            this.btnCrear.BackColor = System.Drawing.Color.Maroon;
            this.btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrear.ForeColor = System.Drawing.Color.White;
            this.btnCrear.Location = new System.Drawing.Point(14, 158);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(256, 35);
            this.btnCrear.TabIndex = 57;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = false;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefono.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTelefono.Location = new System.Drawing.Point(11, 120);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(73, 16);
            this.lblTelefono.TabIndex = 37;
            this.lblTelefono.Text = "Telefono:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.BackColor = System.Drawing.Color.White;
            this.txtTelefono.Location = new System.Drawing.Point(98, 118);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(167, 20);
            this.txtTelefono.TabIndex = 36;
            this.txtTelefono.Tag = "Email";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.SystemColors.Control;
            this.lblEmail.Location = new System.Drawing.Point(33, 96);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(50, 16);
            this.lblEmail.TabIndex = 35;
            this.lblEmail.Text = "Email:";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.SystemColors.Control;
            this.lblNombre.Location = new System.Drawing.Point(16, 67);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(66, 16);
            this.lblNombre.TabIndex = 34;
            this.lblNombre.Text = "Nombre:";
            // 
            // lblApe
            // 
            this.lblApe.AutoSize = true;
            this.lblApe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApe.ForeColor = System.Drawing.SystemColors.Control;
            this.lblApe.Location = new System.Drawing.Point(13, 41);
            this.lblApe.Name = "lblApe";
            this.lblApe.Size = new System.Drawing.Size(69, 16);
            this.lblApe.TabIndex = 33;
            this.lblApe.Text = "Apellido:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(46, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 16);
            this.label2.TabIndex = 32;
            this.label2.Text = "DNI:";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.White;
            this.txtEmail.Location = new System.Drawing.Point(98, 92);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(167, 20);
            this.txtEmail.TabIndex = 27;
            this.txtEmail.Tag = "Email";
            // 
            // txtNom
            // 
            this.txtNom.BackColor = System.Drawing.Color.White;
            this.txtNom.Location = new System.Drawing.Point(98, 66);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(167, 20);
            this.txtNom.TabIndex = 26;
            this.txtNom.Tag = "Nombre";
            // 
            // txtApe
            // 
            this.txtApe.BackColor = System.Drawing.Color.White;
            this.txtApe.Location = new System.Drawing.Point(98, 40);
            this.txtApe.Name = "txtApe";
            this.txtApe.Size = new System.Drawing.Size(167, 20);
            this.txtApe.TabIndex = 25;
            this.txtApe.Tag = "Apellido";
            // 
            // txtDni
            // 
            this.txtDni.BackColor = System.Drawing.Color.White;
            this.txtDni.Location = new System.Drawing.Point(98, 14);
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(167, 20);
            this.txtDni.TabIndex = 24;
            this.txtDni.Tag = "DNI";
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.Maroon;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.Location = new System.Drawing.Point(14, 199);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(256, 35);
            this.btnVolver.TabIndex = 58;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // formCrearCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(314, 281);
            this.Controls.Add(this.panModificarUsuario);
            this.Name = "formCrearCliente";
            this.Text = "formCrearCliente";
            this.Load += new System.EventHandler(this.formCrearCliente_Load);
            this.panModificarUsuario.ResumeLayout(false);
            this.panModificarUsuario.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panModificarUsuario;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblApe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.TextBox txtApe;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnVolver;
    }
}