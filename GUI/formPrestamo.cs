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
    public partial class formPrestamo : Form, IObserver
    {
        private readonly List<Libro> catalogo = new List<Libro>();
        private readonly PrestamoBLL prestamoBLL = new PrestamoBLL();
        private readonly LibroBLL libroBLL = new LibroBLL();
        private readonly UsuarioBLL usuarioBLL = new UsuarioBLL();

        private DateTime fechaProgramada;
        private int DNI_Cli;
        public formPrestamo()
        {
            InitializeComponent();
        }

        private void btnAplicarFiltro_Click(object sender, EventArgs e)
        {
            catalogo.Clear();


            catalogo.AddRange(libroBLL.ListarLibrosPorFiltro(cmbCategoria.Text, cmbAutor.Text, txtTitulo.Text, null, null));

            if (catalogo.Count == 0)
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("msjErrorFiltros"), LanguageManager.Instance.GetTraduction("Error"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dgvCatalogo.DataSource = null;
            dgvCatalogo.DataSource = catalogo;
            dgvCatalogo.ClearSelection();
            dgvCatalogo.Refresh();
        }

        private void btnConfirmarPrestamo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbDiasProgramados.Text) ||
                fechaProgramada == default(DateTime))
            {
                MessageBox.Show(
                    LanguageManager.Instance.GetTraduction("msjCampoFechaVacio"), LanguageManager.Instance.GetTraduction("Prestamo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var libroSel = (Libro)dgvCatalogo.CurrentRow.DataBoundItem;
            Libro libro = libroBLL.ConsultarLibro(libroSel.CodigoInterno);

            Prestamo prestamo = new Prestamo
            {
                Estado = "Pendiente",
                CodigoInterno = libro.CodigoInterno.ToString(),
                FechaProgramada = fechaProgramada,
                FechaEmision = DateTime.Now,
                Dni_Cliente = DNI_Cli,
                Dni_Usuario = Convert.ToInt32(SessionManager.Instance.UsuarioActual().DNI)
            };

            prestamoBLL.RegistrarPrestamo(prestamo);
            MessageBox.Show(LanguageManager.Instance.GetTraduction("msjPrestamoExitoso"), LanguageManager.Instance.GetTraduction("Prestamo"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            //SaveFileDialog saveFileDialog = new SaveFileDialog
            //{
            //    Filter = "Archivo PDF (*.pdf)|*.pdf",
            //    FileName = $"Comprobante_Prestamo_{prestamo.NroPrestamo}.pdf"
            //};

            //if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    // Generar el documento
            //    ComprobanteService.GenerarComprobantePDF(prestamo, saveFileDialog.FileName);

            //    MessageBox.Show(LanguageManager.Instance.GetTraduction("msjComprobanteExitoso"), LanguageManager.Instance.GetTraduction("Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    // Abrir el PDF automáticamente en el visor de PDF por defecto de Windows
            //    System.Diagnostics.Process.Start(saveFileDialog.FileName);
            //}
            dgvCatalogo.DataSource = null;
            dgvCatalogo.DataSource = catalogo;
            dgvCatalogo.ClearSelection();
            dgvCatalogo.Refresh();
        }

        private void dtpFechaProgramada_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpFechaProgramada.Value < DateTime.Now) throw new Exception();
                cmbDiasProgramados.Enabled = false;
                fechaProgramada = dtpFechaProgramada.Value;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        private void cmbDiasProgramados_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtpFechaProgramada.Enabled = false;
            fechaProgramada = prestamoBLL.CalcularFechaDevolucion(int.Parse(cmbDiasProgramados.SelectedItem.ToString()));
        }

        private void formPrestamo_Load(object sender, EventArgs e)
        {
            LanguageManager.Instance.AgregarObservador(this);
            Actualizar(LanguageManager.Instance);

            using (var selectorClientes = new formCarritoClientes())
            {
                if (selectorClientes.ShowDialog(this) != DialogResult.OK)
                    return;

                DNI_Cli = Convert.ToInt32(selectorClientes.DniSeleccionado);
            }

            

            for (int i = 1; i <= 60; i++)
            {
                cmbDiasProgramados.Items.Add(i);
            }

            catalogo.Clear();


            catalogo.AddRange(libroBLL.ListarDisponibles());

            if (catalogo.Count == 0)
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("msjNoHayEjemplares"), LanguageManager.Instance.GetTraduction("Error"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dgvCatalogo.DataSource = null;
            dgvCatalogo.DataSource = catalogo;
            dgvCatalogo.ClearSelection();
            dgvCatalogo.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu menu = new Menu();
            menu.Show();
        }

        public void Actualizar(LanguageManager lenguaje)
        {
            
            lblTituloLibro.Text = LanguageManager.Instance.GetTraduction("lblTituloLibro");
            lblAutorLibro.Text = LanguageManager.Instance.GetTraduction("lblAutorLibro");
            lblCategoriaLibro.Text = LanguageManager.Instance.GetTraduction("lblCategoriaLibro");
            btnVolver.Text = LanguageManager.Instance.GetTraduction("btnVolver");
            btnAplicarFiltro.Text = LanguageManager.Instance.GetTraduction("btnAplicarFiltro");
            btnConfirmarPrestamo.Text = LanguageManager.Instance.GetTraduction("btnConfirmarPrestamo");
            lblFecha.Text = LanguageManager.Instance.GetTraduction("lblFecha");
            lblDias.Text = LanguageManager.Instance.GetTraduction("lblDias");
            lblTituloLibrosPrestamo.Text = LanguageManager.Instance.GetTraduction("lblTituloLibrosPrestamo");
        }
    }
}
