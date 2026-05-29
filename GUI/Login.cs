using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Servicios;

namespace GUI
{
    public partial class Login : Form
    {
        int posX, posY;
        bool arrastrando = false;
        public Login()
        {
            InitializeComponent();
            IniciarControles();
        }

        private void IniciarControles()
        {
            txtContraseña.UseSystemPasswordChar = false;
            MostrarClave(false);
        }

        private void MostrarClave(bool mostrar)
        {
            if (mostrar)
            {
                pbMostrarClave.Image = Properties.Resources.OjoAbierto;
            }else
            {
                pbMostrarClave.Image = Properties.Resources.OjoCerrado;
            }
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

        private void pbMostrarClave_Click(object sender, EventArgs e)
        {
            bool mostrar = txtContraseña.UseSystemPasswordChar;
            if (mostrar)
            {
                MostrarClave(false);
                txtContraseña.UseSystemPasswordChar = false;
            }
            else
            {
                MostrarClave(true);
                txtContraseña.UseSystemPasswordChar = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarCamposVacios();
                UsuarioBLL usuarioBLL = new UsuarioBLL();
                switch (usuarioBLL.Login(txtUsuario.Text, txtContraseña.Text))
                {
                    case LoginResultado.ContraseñaIncorrecta:
                        throw new Exception("Usuario o Contraseña Incorrecta, vuelva a intentar.");
                        
                    case LoginResultado.UsuarioNoEncontrado:
                        throw new Exception("Usuario o Contraseña Incorrecta, vuelva a intentar.");
                    case LoginResultado.Valido:
                        this.Hide();
                        Menu menu = new Menu();
                        menu.ShowDialog();
                        break;
                    case LoginResultado.Bloqueado:
                        throw new Exception("Usuario Bloqueado, contacte al administrador.");
                    case LoginResultado.Error:
                        throw new Exception("Error al intentar iniciar sesión, vuelva a intentar.");
                    default:
                        throw new Exception("Error desconocido, vuelva a intentar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ValidarCamposVacios()
        {
            if(txtUsuario.Text == "") { 
                throw new Exception("Ingrese un Usuario!");
            }
            if (txtContraseña.Text == "")
            {
                throw new Exception("Ingrese una Contraseña!");
            }
        }

        private void linkCambioContraseña_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            CambiarContraseña cambioContraseña = new CambiarContraseña();
            cambioContraseña.ShowDialog();
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
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
