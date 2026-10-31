using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class formClientes : Form
    {
        private UserAction userAction;

        ClienteBLL clienteBLL = new ClienteBLL();
        public formClientes()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SetModoEdicion(bool enEdicion)
        {
            // Botones de acción: deshabilitados durante edición
            btnCrear.Enabled = !enEdicion;
            btnModificar.Enabled = !enEdicion;
            btnBorrar.Enabled = !enEdicion;

            btnCrear.BackColor = !enEdicion ? Color.Green : Color.Maroon;
            btnModificar.BackColor = !enEdicion ? Color.Green : Color.Maroon;
            btnBorrar.BackColor = !enEdicion ? Color.Green : Color.Maroon;

            // Aplicar/Cancelar: solo activos durante edición
            btnAplicar.Enabled = enEdicion;
            btnCancelar.Enabled = enEdicion;
            btnAplicar.BackColor = enEdicion ? Color.Green : Color.Maroon;
            btnCancelar.BackColor = enEdicion ? Color.Green : Color.Maroon;
            panel10.BackColor = enEdicion ? Color.Green : Color.Maroon;

            // Panel de filtrado y formulario
            panel2.Enabled = !enEdicion;
            panModificarUsuario.Enabled = enEdicion;
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            UserAction userAction = UserAction.Add;

            SetModoEdicion(true);
        }

        private void panModificarUsuario_EnabledChanged(object sender, EventArgs e)
        {
            Color color = panModificarUsuario.Enabled ? Color.White : Color.Gray;
            txtDni.BackColor = color;
            txtNom.BackColor = color;
            txtApe.BackColor = color;
            txtEmail.BackColor = color;
            txtTelefono.BackColor = color;
        }

        private void btnCancelar_Click(object sender, EventArgs e) => ReiniciarBotones();

        private void ReiniciarBotones()
        {
            txtNom.Clear();
            txtApe.Clear();
            txtDni.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            txtDni.ReadOnly = false;

            SetModoEdicion(false);
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (userAction)
                {
                    case UserAction.Add:
                    if(ValidarCamposVacios(txtDni, txtApe, txtNom, txtEmail, txtTelefono))
                        {
                            clienteBLL.CrearCliente(new Cliente
                            {
                                DNI = Convert.ToInt32(txtDni.Text),
                                Nombre = txtNom.Text,
                                Apellido = txtApe.Text,
                                Telefono = txtTelefono.Text,
                                    Email = txtEmail.Text,
                                    Activo = (dgvClientes.SelectedRows[0].DataBoundItem as Cliente).Activo
                            });
                            ReiniciarBotones();
                            MostrarClientes(dgvClientes, clienteBLL.ListarClientes());
                            MessageBox.Show(LanguageManager.Instance.GetTraduction("ClientCreado"),
                            LanguageManager.Instance.GetTraduction("Alerta"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        
                        break;
                    case UserAction.Delete:
                        clienteBLL.BorrarCliente((dgvClientes.SelectedRows[0].DataBoundItem as Cliente).DNI.ToString());
                        ReiniciarBotones();
                        MostrarClientes(dgvClientes, clienteBLL.ListarClientes());
                        MessageBox.Show(LanguageManager.Instance.GetTraduction("ClientBorrado"),
                        LanguageManager.Instance.GetTraduction("Alerta"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                        break;
                    case UserAction.Modify:
                        if (ValidarCamposVacios(txtNom, txtApe, txtDni, txtEmail, txtTelefono) && ValidarEntradaUsuario())
                        {
                            string dniOriginal = (dgvClientes.SelectedRows[0].DataBoundItem as Cliente).DNI.ToString();

                            clienteBLL.ModificarCliente(new Cliente 
                                { 
                                    DNI = Convert.ToInt32(dniOriginal), 
                                    Nombre = txtNom.Text, 
                                    Apellido = txtApe.Text, 
                                    Telefono = txtTelefono.Text, 
                                    Email = txtEmail.Text 
                                });

                            ReiniciarBotones();
                            MostrarClientes(dgvClientes, clienteBLL.ListarClientes());
                            MessageBox.Show(LanguageManager.Instance.GetTraduction("ClientModif"),
                            LanguageManager.Instance.GetTraduction("Alerta"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (dgvClientes.SelectedRows.Count == 0 || dgvClientes.SelectedRows[0].Cells[0].Value == null)
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("UserMsj4")); // "Seleccione un usuario" o similar
                return;
            }

            userAction = UserAction.Modify;
            SetModoEdicion(true);

            Cliente cliente = clienteBLL.RetornarCliente(dgvClientes.SelectedRows[0].Cells[0].Value.ToString());

            if (cliente == null) return;

            txtDni.Text = cliente.DNI.ToString();
            txtNom.Text = cliente.Nombre;
            txtApe.Text = cliente.Apellido;
            txtTelefono.Text = cliente.Telefono;
            txtEmail.Text = cliente.Email;
            txtDni.ReadOnly = true;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            userAction = UserAction.Delete;
            SetModoEdicion(true);

            Cliente cliente = clienteBLL.RetornarCliente(dgvClientes.SelectedRows[0].Cells[0].Value.ToString());

            if (cliente == null) return;
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
        public void MostrarClientes(DataGridView dgv, object obj)
        {
            dgv.DataSource = null;
            dgv.DataSource = obj;
        }

        private void formClientes_Load(object sender, EventArgs e)
        {
            MostrarClientes(dgvClientes, clienteBLL.ListarClientes());

            
            dgvClientes.Columns[nameof(Cliente.Activo)].Visible = false;
            dgvClientes.Columns[nameof(Cliente.DVH)].Visible = false;

            dgvClientes.Columns[nameof(Cliente.DNI)].DisplayIndex = 0;
            dgvClientes.Columns[nameof(Cliente.Nombre)].DisplayIndex = 1;
            dgvClientes.Columns[nameof(Cliente.Apellido)].DisplayIndex = 2;
            dgvClientes.Columns[nameof(Cliente.Email)].DisplayIndex = 3;
            dgvClientes.Columns[nameof(Cliente.Telefono)].DisplayIndex = 4;

            Actualizar(LanguageManager.Instance);
        }

        public void Actualizar(LanguageManager lenguaje)
        {
            lblTituloClientes.Text = lenguaje.GetTraduction("lblTituloClientes");
            txtDni.Text = lenguaje.GetTraduction("DNI");
            lblApe.Text = lenguaje.GetTraduction("lblApe");
            lblNombre.Text = lenguaje.GetTraduction("lblNombre");
            lblEmail.Text = lenguaje.GetTraduction("lblEmail");
            lblTelefono.Text = lenguaje.GetTraduction("lblTelefono");
            lblOpciones.Text = lenguaje.GetTraduction("lblOpciones");

            btnCrear.Text = lenguaje.GetTraduction("btnCrear");
            btnModificar.Text = lenguaje.GetTraduction("btnModificar");
            btnBorrar.Text = lenguaje.GetTraduction("btnBorrar");
            btnAplicar.Text = lenguaje.GetTraduction("btnAplicar");
            btnCancelar.Text = lenguaje.GetTraduction("btnCancelar");
            btnSalir.Text = lenguaje.GetTraduction("btnSalir");
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (userAction == UserAction.Delete || userAction == UserAction.Modify || userAction == UserAction.UnBlock)
            {
                if (dgvClientes.SelectedRows.Count > 0 && dgvClientes.SelectedRows[0].Cells[0].Value != null)
                {
                    Cliente cliente = clienteBLL.RetornarCliente(
                        dgvClientes.SelectedRows[0].Cells[0].Value.ToString());
                    if (cliente == null) return;

                    txtDni.Text = cliente.DNI.ToString();
                    txtDni.ReadOnly = (userAction == UserAction.Modify); // mantiene el lock si estamos modificando
                    txtNom.Text = cliente.Nombre;
                    txtApe.Text = cliente.Apellido;
                    txtEmail.Text = cliente.Email;
                    txtTelefono.Text = cliente.Telefono;
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.Show();
        }
    }
}
