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
    public partial class formCarritoClientes : Form
    {
        ClienteBLL clienteBLL = new ClienteBLL();
        public string DniSeleccionado { get; private set; }

        public formCarritoClientes()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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
            btnVolver.Text = lenguaje.GetTraduction("btnVolver");
            btnSeleccionar.Text = lenguaje.GetTraduction("btnSeleccionar");
        }
        public void MostrarClientes(DataGridView dgv, object obj)
        {
            dgv.DataSource = null;
            dgv.DataSource = obj;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null || dgvClientes.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Seleccione un cliente.", "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Cliente cliente = dgvClientes.CurrentRow.DataBoundItem as Cliente;
            if (cliente == null) return;

            DniSeleccionado = cliente.DNI.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string dniBusqueda = txtDni.Text.Trim();    

                if (!Regex.IsMatch(dniBusqueda, @"^\d{8}$")) 
                {
                    MessageBox.Show(LanguageManager.Instance.GetTraduction("CampoDni"), LanguageManager.Instance.GetTraduction("ErrorEntradaDeDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                    

                Cliente cliente = clienteBLL.RetornarCliente(dniBusqueda);

                if (cliente != null && cliente.Activo == 1)
                {
                    MostrarClientes(dgvClientes, new List<Cliente> { cliente });
                    dgvClientes.CurrentCell = dgvClientes.Rows[0].Cells[nameof(Cliente.DNI)];
                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    LanguageManager.Instance.GetTraduction("MsjNoEncotroCliente"),
                    LanguageManager.Instance.GetTraduction("AlertaClienteNoEncontrado"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                    return;

                using (var crearCliente = new formCrearCliente(dniBusqueda))
                {
                    if (crearCliente.ShowDialog(this) == DialogResult.OK)
                    {
                        DniSeleccionado = crearCliente.DniCreado;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
