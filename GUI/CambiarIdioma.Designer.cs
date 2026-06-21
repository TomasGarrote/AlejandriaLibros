namespace GUI
{
    partial class CambiarIdioma
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
            this.lblCambiarIdioma = new System.Windows.Forms.Label();
            this.cbIdiomas = new System.Windows.Forms.ComboBox();
            this.btnCambiarCI = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCambiarIdioma
            // 
            this.lblCambiarIdioma.AutoSize = true;
            this.lblCambiarIdioma.Location = new System.Drawing.Point(245, 95);
            this.lblCambiarIdioma.Name = "lblCambiarIdioma";
            this.lblCambiarIdioma.Size = new System.Drawing.Size(79, 13);
            this.lblCambiarIdioma.TabIndex = 0;
            this.lblCambiarIdioma.Text = "Cambiar Idioma";
            // 
            // cbIdiomas
            // 
            this.cbIdiomas.FormattingEnabled = true;
            this.cbIdiomas.Location = new System.Drawing.Point(224, 111);
            this.cbIdiomas.Name = "cbIdiomas";
            this.cbIdiomas.Size = new System.Drawing.Size(121, 21);
            this.cbIdiomas.TabIndex = 1;
            // 
            // btnCambiarCI
            // 
            this.btnCambiarCI.Location = new System.Drawing.Point(224, 138);
            this.btnCambiarCI.Name = "btnCambiarCI";
            this.btnCambiarCI.Size = new System.Drawing.Size(121, 23);
            this.btnCambiarCI.TabIndex = 2;
            this.btnCambiarCI.Text = "Cambiar";
            this.btnCambiarCI.UseVisualStyleBackColor = true;
            this.btnCambiarCI.Click += new System.EventHandler(this.btnCambiar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(249, 202);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CambiarIdioma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 426);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCambiarCI);
            this.Controls.Add(this.cbIdiomas);
            this.Controls.Add(this.lblCambiarIdioma);
            this.Name = "CambiarIdioma";
            this.Text = "CambiarIdioma";
            this.Load += new System.EventHandler(this.CambiarIdioma_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCambiarIdioma;
        private System.Windows.Forms.ComboBox cbIdiomas;
        private System.Windows.Forms.Button btnCambiarCI;
        private System.Windows.Forms.Button button1;
    }
}