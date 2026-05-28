using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Usuarios : Form
    {
        int posX, posY;
        bool arrastrando = false;
        public Usuarios()
        {
            InitializeComponent();
            lblTextoTabla.Text = "[Usuarios Activos]";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                lblTextoTabla.Text = "[Usuarios Activos]";
            }
            else
            {
                lblTextoTabla.Text = "[Todos los Usuarios]";
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu menu = new Menu();
            menu.Show();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            arrastrando = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastrando)
            {
                this.Location = new Point(this.Location.X + (e.X - posX), this.Location.Y + (e.Y - posY));
            }
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panModificarUsuario_EnabledChanged(object sender, EventArgs e)
        {
            if (panModificarUsuario.Enabled == true)
            {
                txtDni.BackColor = Color.White;
                txtNom.BackColor = Color.White;
                txtApe.BackColor =  Color.White;
                txtEmail.BackColor = Color.White;
                txtUsuario.BackColor = Color.White;
                txtRol.BackColor = Color.White;
                cmbActivo.BackColor = Color.White;
                cmbBloqueado.BackColor = Color.White;
            }
            else
            {
                txtDni.BackColor = Color.Gray;
                txtNom.BackColor = Color.Gray;
                txtApe.BackColor =  Color.Gray;
                txtEmail.BackColor = Color.Gray;
                txtUsuario.BackColor = Color.Gray;
                txtRol.BackColor = Color.Gray;
                cmbActivo.BackColor = Color.Gray;
                cmbBloqueado.BackColor = Color.Gray;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                arrastrando = true;
                posX = e.X;
                posY = e.Y;
            }
        }
    }
}
