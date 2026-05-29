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
    public partial class CambiarContraseña : Form
    {
        public CambiarContraseña()
        {
            InitializeComponent();
        }

        private void btnGuardarContra_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarCamposVacios();
                UsuarioBLL usuarioBLL = new UsuarioBLL();
                
                switch (usuarioBLL.CambiarClave(txtUsuario.Text, txtContraseña.Text, txtNuevaContraseña.Text))
                {
                    case LoginResultado.Valido:
                        MessageBox.Show("Contraseña cambiada con éxito!", "Éxito!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        Login login = new Login();
                        login.Show();
                        break;
                    case LoginResultado.ContraseñaIguales:
                        MessageBox.Show("La nueva contraseña no puede ser igual a la anterior!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case LoginResultado.UsuarioNoEncontrado:
                        MessageBox.Show("Hay un error en Usuario!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case LoginResultado.ContraseñaIncorrecta:
                        MessageBox.Show("Hay un error en Contraseña!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case LoginResultado.Bloqueado:
                        MessageBox.Show("El usuario se encuentra bloqueado, contacte al administrador!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void ValidarCamposVacios()
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Focus();
                throw new Exception("Ingrese su Usuario!");
            }
            if (txtContraseña.Text == "")
            {
                throw new Exception("Ingrese su Contraseña!");
            }
            if (txtNuevaContraseña.Text == "")
            {
                throw new Exception("Ingrese una nueva Contraseña!");
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

        private void linkVolver_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.Show();
        }

        private void pbMostrarClave_Click(object sender, EventArgs e)
        {
            bool mostrar = txtContraseña.UseSystemPasswordChar;
            if (mostrar)
            {
                MostrarClave(false);
                txtContraseña.UseSystemPasswordChar = false;
                txtNuevaContraseña.UseSystemPasswordChar = false;

            }
            else
            {
                MostrarClave(true);
                txtContraseña.UseSystemPasswordChar = true;
                txtNuevaContraseña.UseSystemPasswordChar = true;
            }
        }
        private void MostrarClave(bool mostrar)
        {
            if (mostrar)
            {
                pbMostrarClave.Image = Properties.Resources.OjoAbierto;
            }
            else
            {
                pbMostrarClave.Image = Properties.Resources.OjoCerrado;
            }
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

        private void txtNuevaContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }
    }
}
