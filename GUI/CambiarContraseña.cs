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
    public partial class CambiarContraseña : Form,IObserver
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
                        MessageBox.Show(LanguageManager.Instance.GetTraduction("CamClaMsj1"), LanguageManager.Instance.GetTraduction("ExitoP"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if(SessionManager.Instance.Logueado())
                        {
                            usuarioBLL.Desloguear();
                        }
                        
                        this.Close();
                        Login login = new Login();
                        login.Show();
                        break;
                    case LoginResultado.ContraseñaIguales:
                        MessageBox.Show(LanguageManager.Instance.GetTraduction("CamClaMsj2"), LanguageManager.Instance.GetTraduction("ErrorP"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case LoginResultado.UsuarioNoEncontrado:
                        MessageBox.Show(LanguageManager.Instance.GetTraduction("CamClaMsj3"), LanguageManager.Instance.GetTraduction("ErrorP"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case LoginResultado.ContraseñaIncorrecta:
                        MessageBox.Show(LanguageManager.Instance.GetTraduction("CamClaMsj4"), LanguageManager.Instance.GetTraduction("ErrorP"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case LoginResultado.Bloqueado:
                        MessageBox.Show(LanguageManager.Instance.GetTraduction("CamMsj5"), LanguageManager.Instance.GetTraduction("ErrorP"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LanguageManager.Instance.GetTraduction("ErrorP"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void ValidarCamposVacios()
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Focus();
                throw new Exception(LanguageManager.Instance.GetTraduction("CamMsj6"));
            }
            if (txtContraseña.Text == "")
            {
                throw new Exception(LanguageManager.Instance.GetTraduction("CamMsj7"));
            }
            if (txtNuevaContraseña.Text == "")
            {
                throw new Exception(LanguageManager.Instance.GetTraduction("CamMsj8"));
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
            if(SessionManager.Instance.UsuarioActual != null)
            {
                this.Close();
                Menu menu = new Menu();
                menu.Show();
            }
            else
            {
                this.Close();
                Login login = new Login();
                login.Show();
            }
                
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

        public void Actualizar(LanguageManager lenguaje)
        {
            lblUsuarioFCC.Text = lenguaje.GetTraduction("lblUsuarioFCC");
            lblContraseñaActual.Text = lenguaje.GetTraduction("lblContraseñaActual");
            lblContraseñaFCC.Text = lenguaje.GetTraduction("lblContraseñaFCC");

            btnGuardarContra.Text = lenguaje.GetTraduction("btnGuardarContra");
            linkVolver.Text = lenguaje.GetTraduction("linkVolver");
        }

        private void CambiarContraseña_Load(object sender, EventArgs e)
        {
            LanguageManager.Instance.AgregarObservador(this);
            Actualizar(LanguageManager.Instance);
        }
    }
}
