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
    public partial class formDevolucion : Form, IObserver
    {
        private readonly List<Prestamo> listaPrestamos = new List<Prestamo>();
        private readonly PrestamoBLL prestamoBLL = new PrestamoBLL();
        public formDevolucion()
        {
            InitializeComponent();
        }

        private void btnAplicarFiltro_Click(object sender, EventArgs e)
        {
            listaPrestamos.Clear();


            listaPrestamos.AddRange(prestamoBLL.ConsultarHistorial(txtDniCliente.Text, "Pendiente", txtTitulo.Text, DateTime.MinValue, DateTime.MinValue));

            if (listaPrestamos.Count == 0)
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("msjErrorFiltros"), LanguageManager.Instance.GetTraduction("Error"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dgvPrestamosPendientes.DataSource = null;
            dgvPrestamosPendientes.DataSource = listaPrestamos;
            dgvPrestamosPendientes.ClearSelection();
            dgvPrestamosPendientes.Refresh();
        }

        private void btnDevolucion_Click(object sender, EventArgs e)
        {
            try
            {
                var objDevolucion = (Prestamo)dgvPrestamosPendientes.CurrentRow.DataBoundItem;

                Prestamo devolucion = prestamoBLL.RetornarPrestamo(objDevolucion.NroPrestamo);

                prestamoBLL.RegistrarDevolucion(devolucion);
                MessageBox.Show(LanguageManager.Instance.GetTraduction("msjDevolucionExitosa"), LanguageManager.Instance.GetTraduction("Prestamo"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                //SaveFileDialog saveFileDialog = new SaveFileDialog
                //{
                //    Filter = "Archivo PDF (*.pdf)|*.pdf",
                //    FileName = $"Comprobante_Devolucion_{devolucion.NroPrestamo}.pdf"
                //};

                //if (saveFileDialog.ShowDialog() == DialogResult.OK)
                //{
                //    // Generar el documento
                //    ComprobanteService.GenerarComprobantePDF(devolucion, saveFileDialog.FileName);

                //    MessageBox.Show(LanguageManager.Instance.GetTraduction("msjComprobanteExitoso"), LanguageManager.Instance.GetTraduction("Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                //    // Abrir el PDF automáticamente en el visor de PDF por defecto de Windows
                //    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                //}
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void formDevolucion_Load(object sender, EventArgs e)
        {
            LanguageManager.Instance.AgregarObservador(this);
            Actualizar(LanguageManager.Instance);

            listaPrestamos.Clear();


            listaPrestamos.AddRange(prestamoBLL.ListarPrestamosPendientes());

            if (listaPrestamos.Count == 0)
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("msjNoHayPrestamosPendientes"), LanguageManager.Instance.GetTraduction("Error"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dgvPrestamosPendientes.DataSource = null;
            dgvPrestamosPendientes.DataSource = listaPrestamos;
            dgvPrestamosPendientes.ClearSelection();
            dgvPrestamosPendientes.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu menu = new Menu();
            menu.Show();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtTitulo.Text = "";
            txtDniCliente.Text = "";
        }
        public void Actualizar(LanguageManager lenguaje)
        {
            btnConfirmarDevolucion.Text = LanguageManager.Instance.GetTraduction("btnConfirmarDevolucion");
            lblTituloPrestamosPendientes.Text = LanguageManager.Instance.GetTraduction("lblTituloPrestamosPendientes");
            lblDNICliente.Text = LanguageManager.Instance.GetTraduction("lblDNICliente");

            btnVolver.Text = LanguageManager.Instance.GetTraduction("btnVolver");

            btnAplicarFiltro.Text = LanguageManager.Instance.GetTraduction("btnAplicarFiltro");
            lblTitulo.Text = LanguageManager.Instance.GetTraduction("lblTituloLibro");
        }
    }
}
