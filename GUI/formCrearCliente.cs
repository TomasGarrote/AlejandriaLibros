using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class formCrearCliente : Form
    {
        private readonly ClienteBLL clienteBLL = new ClienteBLL();
        public string DniCreado { get; private set; }

        public formCrearCliente()
        {
            InitializeComponent();
        }

        public formCrearCliente(string dniInicial) : this()
        {
            txtDni.Text = dniInicial;
            txtDni.ReadOnly = true;
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCamposVacios(txtNom, txtApe, txtDni, txtEmail, txtTelefono) && ValidarEntradaUsuario())
                {
                    clienteBLL.CrearCliente(new Cliente
                    {
                        DNI = Convert.ToInt32(txtDni.Text.Trim()),
                        Nombre = txtNom.Text.Trim(),
                        Apellido = txtApe.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Telefono = txtTelefono.Text.Trim(),
                        Activo = 1
                    });

                    DniCreado = txtDni.Text.Trim();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LanguageManager.Instance.GetTraduction("msjErrorCrearCliente"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidarCamposVacios(params Control[] controles)
        {
            StringBuilder mensaje = new StringBuilder();
            bool hayVacios = false;
            Control primerInvalido = null;

            foreach (Control c in controles)
            {
                string nombreCampo = c.Tag?.ToString() ?? c.Name;
                if (c is TextBox txt && string.IsNullOrWhiteSpace(txt.Text))
                {
                    mensaje.AppendLine($"{LanguageManager.Instance.GetTraduction("ElCampo")} " + " " + nombreCampo + " " + LanguageManager.Instance.GetTraduction("EstaVacio"));
                    if (primerInvalido == null) primerInvalido = txt;
                    hayVacios = true;
                }
                else if (c is ComboBox cb && cb.SelectedIndex == -1)
                {
                    mensaje.AppendLine($"{LanguageManager.Instance.GetTraduction("ElCampo")}" + " " + nombreCampo + " " + LanguageManager.Instance.GetTraduction("NoFueSelecc"));
                    if (primerInvalido == null) primerInvalido = cb;
                    hayVacios = true;
                }
            }

            if (hayVacios)
            {
                MessageBox.Show(mensaje.ToString(), LanguageManager.Instance.GetTraduction("FaltaCompl"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                primerInvalido?.Focus(); // Enfocar el primer campo con error
            }
            return !hayVacios;
        }
        private bool ValidarEntradaUsuario()
        {
            StringBuilder errores = new StringBuilder();

            if (!Regex.IsMatch(txtNom.Text.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$"))
                errores.AppendLine(LanguageManager.Instance.GetTraduction("CampoNombre"));

            if (!Regex.IsMatch(txtApe.Text.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$"))
                errores.AppendLine(LanguageManager.Instance.GetTraduction("CampoApell"));


            if (!Regex.IsMatch(txtDni.Text.Trim(), @"^\d{8}$"))
                errores.AppendLine(LanguageManager.Instance.GetTraduction("CampoDni"));

            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errores.AppendLine(LanguageManager.Instance.GetTraduction("ElCorreoElec"));

            if (errores.Length > 0)
            {
                MessageBox.Show(errores.ToString(), LanguageManager.Instance.GetTraduction("ErrorEntradaDeDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public void Actualizar(LanguageManager lenguaje)
        {
            lblApe.Text = lenguaje.GetTraduction("lblApe");
            lblNombre.Text = lenguaje.GetTraduction("lblNombre");
            lblEmail.Text = lenguaje.GetTraduction("lblEmail");
            lblTelefono.Text = lenguaje.GetTraduction("lblTelefono");

            btnCrear.Text = lenguaje.GetTraduction("btnCrear");
            btnVolver.Text = lenguaje.GetTraduction("btnVolver");
        }

        private void formCrearCliente_Load(object sender, EventArgs e)
        {
            Actualizar(LanguageManager.Instance);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
