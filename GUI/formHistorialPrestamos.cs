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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class formHistorialPrestamos : Form, IObserver
    {
        private readonly List<Prestamo> listaPrestamos = new List<Prestamo>();
        private readonly PrestamoBLL prestamoBLL = new PrestamoBLL();
        public formHistorialPrestamos()
        {
            InitializeComponent();
        }

        private void formHistorialPrestamos_Load(object sender, EventArgs e)
        {
            LanguageManager.Instance.AgregarObservador(this);
            Actualizar(LanguageManager.Instance);
            listaPrestamos.Clear();

            cmbEstado.Items.AddRange(prestamoBLL.ListarEstados());
            listaPrestamos.AddRange(prestamoBLL.ListarTodosLosPrestamos());

            if (listaPrestamos.Count == 0)
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("msjNoHayPrestamos"), LanguageManager.Instance.GetTraduction("Error"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dgvPrestamos.DataSource = null;
            dgvPrestamos.DataSource = listaPrestamos;
            dgvPrestamos.ClearSelection();
            dgvPrestamos.Refresh();
        }

        private void btnAplicarFiltro_Click(object sender, EventArgs e)
        {
            listaPrestamos.Clear();


            listaPrestamos.AddRange(prestamoBLL.ConsultarHistorial("", cmbEstado.Text, txtTitulo.Text, DateTime.MinValue, DateTime.MinValue));

            if (listaPrestamos.Count == 0)
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("msjErrorFiltros"), LanguageManager.Instance.GetTraduction("Error"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dgvPrestamos.DataSource = null;
            dgvPrestamos.DataSource = listaPrestamos;
            dgvPrestamos.ClearSelection();
            dgvPrestamos.Refresh();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu menu = new Menu();
            menu.Show();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtTitulo.Text = "";
            txtDni.Text = "";
            cmbEstado.Text = "";
            dtpDesde.Value = DateTime.MinValue;
            dtpHasta.Value = DateTime.MinValue;

        }
        public void Actualizar(LanguageManager lenguaje)
        {
            lblPrestamos.Text = LanguageManager.Instance.GetTraduction("lblPrestamos");
            lblDesdeFecha.Text = LanguageManager.Instance.GetTraduction("lblDesdeFecha");
            lblHastaFecha.Text = LanguageManager.Instance.GetTraduction("lblHastaFecha");
            lblEstado.Text = LanguageManager.Instance.GetTraduction("lblEstado");
            lblPrestamos.Text = LanguageManager.Instance.GetTraduction("lblPrestamos");
            btnVolver.Text = LanguageManager.Instance.GetTraduction("btnVolver");
            lblTituloLibro.Text = LanguageManager.Instance.GetTraduction("lblTituloLibro");

            btnAplicarFiltro.Text = LanguageManager.Instance.GetTraduction("btnAplicarFiltro");
        }
    }
}
