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
                switch (usuarioBLL.Login(txtUsuario.Text, txtContraseña.Text))
                {
                    case LoginResultado.Valido:
                        usuarioBLL.Desloguear();
                        usuarioBLL.CambiarClave(txtUsuario.Text, txtNuevaContraseña.Text);
                        
                        MessageBox.Show("Contraseña cambiada con éxito!", "Éxito!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        Login login = new Login();
                        login.Show();
                        break;
                    case LoginResultado.UsuarioNoEncontrado:
                        MessageBox.Show("Hay un error en Usuario o Contraseña!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case LoginResultado.ContraseñaIncorrecta:
                        MessageBox.Show("Hay un error en Usuario o Contraseña!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
