namespace GUI
{
    partial class FrmConexionInicial
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
            this.cmbServidor = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbServidor
            // 
            this.cmbServidor.FormattingEnabled = true;
            this.cmbServidor.Location = new System.Drawing.Point(35, 26);
            this.cmbServidor.Name = "cmbServidor";
            this.cmbServidor.Size = new System.Drawing.Size(155, 21);
            this.cmbServidor.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(34, 85);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(115, 85);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(75, 23);
            this.btnConectar.TabIndex = 3;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // FrmConexionInicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 163);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.cmbServidor);
            this.Name = "FrmConexionInicial";
            this.Text = "FrmConexionInicial";
            this.Load += new System.EventHandler(this.FrmConexionInicial_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbServidor;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnConectar;
    }
}