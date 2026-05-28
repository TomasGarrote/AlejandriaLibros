using BLL;
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
    public partial class Usuarios : Form
    {
        int posX, posY;
        bool arrastrando = false;
        private UserAction userAction;
        private UsuarioBLL usuariBLL;
        public Usuarios()
        {
            InitializeComponent();
            usuariBLL = new UsuarioBLL();
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
                
        //Crear Usuario
        private void button1_Click(object sender, EventArgs e)
        {
            userAction = UserAction.Add;
            textBox1.Text = "Modo Añadir";
            EnabledControls(button1, button2, button3, button4, btnAplicar, btnCancelar, button7, panel2, panModificarUsuario, panel3);
            usuariBLL.RegistrarUsuario(new UsuarioBE());
        }
        private void EnabledControls(params Control[] controls)
        {
            foreach (Control b in controls)
            {
                b.Enabled = !b.Enabled;
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (userAction)
                {
                    case UserAction.Add:
                        usuariBLL.RegistrarUsuario(new )

                        break;
                    case UserAction.Delete:
                        break;
                    case UserAction.Modify:
                        break;
                    case UserAction.UnBlock:
                        break;
                    case UserAction.Consult:
                        break;
                    default:
                        break;

                }
            }
            catch (Exception ex)
            {

                throw;
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
