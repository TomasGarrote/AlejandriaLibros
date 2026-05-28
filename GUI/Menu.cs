using Servicios;
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
    public partial class Menu : Form
    {
        int posX, posY;
        bool arrastrando = false;
        public Menu()
        {
            InitializeComponent();
            customMenu();
        }
        private void customMenu()
        {
            panelAdminSubmenu.Visible = false;
            panelUsuarioSubmenu.Visible = false;

        }
        private void esconderSubmenu()
        {
            if (panelAdminSubmenu.Visible == true)
                panelAdminSubmenu.Visible = false;
            if (panelUsuarioSubmenu.Visible == true)
                panelUsuarioSubmenu.Visible = false;
        }
        private void mostrarSubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                esconderSubmenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            mostrarSubmenu(panelAdminSubmenu);
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            mostrarSubmenu(panelUsuarioSubmenu);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            customMenu();
        }

        private void btnAdmin_Click_1(object sender, EventArgs e)
        {
            mostrarSubmenu(panelAdminSubmenu);
        }

        private void btnUsuario_Click_1(object sender, EventArgs e)
        {
            mostrarSubmenu(panelUsuarioSubmenu);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                arrastrando = true;
                posX = e.X;
                posY = e.Y;
            }
        }

        private void BarraTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastrando)
            {
                this.Location = new Point(this.Location.X + (e.X - posX), this.Location.Y + (e.Y - posY));
            }
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormBitacora bitacora = new FormBitacora();
            bitacora.Show();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            this.Hide();
            Usuarios usuarios = new Usuarios();
        }

        private void BarraTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            arrastrando = false;
        }
    }
}
