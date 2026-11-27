using BLL;
using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servicios;

namespace GUI
{
    public partial class frmBitacoraDeCambios : Form
    {
        BitacoraDeCambiosBLL bll;
        public frmBitacoraDeCambios()
        {
            bll = new BitacoraDeCambiosBLL();
            InitializeComponent();
        }

        private void frmBitacoraDeCambios_Load(object sender, EventArgs e)
        {
            dgvBitacoraCambios.DataSource = bll.ListarTodoElHistorial();
        }

        private void btnAplicarB_Click(object sender, EventArgs e)
        {
            if(txtDNI.Text.Length > 0 && !int.TryParse(txtDNI.Text, out _))
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("ErroValidacionDNI"), "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("ErrorValidacionFecha"), LanguageManager.Instance.GetTraduction("ErrorValidacion"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<ClienteHistorial> historialFiltrado = bll.FiltrarHistorial(
                string.IsNullOrWhiteSpace(txtDNI.Text) ? (int?)null : int.Parse(txtDNI.Text),
                string.IsNullOrWhiteSpace(txtNombre.Text) ? null : txtNombre.Text,
                string.IsNullOrWhiteSpace(txtApellido.Text) ? null : txtApellido.Text,
                dtpDesde.Checked ? dtpDesde.Value.Date : (DateTime?)null,
                dtpHasta.Checked ? dtpHasta.Value.Date : (DateTime?)null
            );
            dgvBitacoraCambios.DataSource = historialFiltrado;

            LimpiarFiltros();
        }

        private void LimpiarFiltros()
        {
            txtDNI.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            dtpDesde.Checked = false;
            dtpHasta.Checked = false;
        }

        private void btnLimpiarB_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
            dgvBitacoraCambios.DataSource = bll.ListarTodoElHistorial();
        }

        private void dgvBitacoraCambios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvBitacoraCambios.Rows.Count)
            {
                DataGridViewRow row = dgvBitacoraCambios.Rows[e.RowIndex];

                var item = row.DataBoundItem as ClienteHistorial;

                if (item != null && item.VersionActiva)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(220, 255, 220);
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkGreen;

                    row.DefaultCellStyle.Font = new System.Drawing.Font(dgvBitacoraCambios.Font, System.Drawing.FontStyle.Bold);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.Gray;
                    row.DefaultCellStyle.Font = new System.Drawing.Font(dgvBitacoraCambios.Font, System.Drawing.FontStyle.Regular);
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Menu"] != null)
            {
                Application.OpenForms["Menu"].Show();
            }
            else
            {
                Menu menu = new Menu();
                menu.Show();
            }
            this.Close();
        }

        private void dgvBitacoraCambios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBitacoraCambios.CurrentRow != null && dgvBitacoraCambios.CurrentRow.DataBoundItem != null)
            {
                var clienteHistorial = dgvBitacoraCambios.CurrentRow.DataBoundItem as ClienteHistorial;

                if (clienteHistorial != null)
                {
                    btnActivarVersion.Enabled = !clienteHistorial.VersionActiva;
                }
            }
            else
            {
                btnActivarVersion.Enabled = false;
            }
        }

        private void btnActivarVersion_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBitacoraCambios.CurrentRow == null || dgvBitacoraCambios.CurrentRow.DataBoundItem == null)
                {
                    MessageBox.Show(LanguageManager.Instance.GetTraduction("AtencionUsuarioActivarVersion"), LanguageManager.Instance.GetTraduction("Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var historialSeleccionado = dgvBitacoraCambios.CurrentRow.DataBoundItem as ClienteHistorial;

                if (historialSeleccionado == null) return;

                DialogResult resultado = MessageBox.Show(
                $"¿Está seguro de que desea restaurar y activar la versión del cliente {historialSeleccionado.Nombre} {historialSeleccionado.Apellido} (DNI: {historialSeleccionado.DNI}) correspondientes al cambio del {historialSeleccionado.FechaCambio:dd/MM/yyyy HH:mm}?",
                "Confirmar Activación de Versión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );

                if (resultado == DialogResult.Yes)
                {
                    string usuarioLogueado = SessionManager.Instance.UsuarioActual().Username;

                    bll.ActivarVersionCliente(historialSeleccionado, usuarioLogueado);

                    MessageBox.Show(LanguageManager.Instance.GetTraduction("ExitoVersionUsuarioActivada"), LanguageManager.Instance.GetTraduction("Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dgvBitacoraCambios.DataSource = bll.ListarTodoElHistorial();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("ErrorActivarVersionUsuario") + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
