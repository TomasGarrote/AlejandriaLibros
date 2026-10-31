// FormVenta.cs
using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormVenta : Form, IObserver
    {
        private readonly VentaBLL ventaBLL = new VentaBLL();
        private readonly LibroBLL libroBLL = new LibroBLL();
        private readonly UsuarioBLL usuarioBLL = new UsuarioBLL();
        private readonly List<Libro> catalogo = new List<Libro>();
        private readonly BindingList<DetalleVenta> carrito = new BindingList<DetalleVenta>();
        private int nroVentaConfirmada = -1;
        private readonly string dniEmpleadoActual; 
        private string dniClienteSeleccionado;

        public FormVenta(string dniEmpleado)
        {
            InitializeComponent();
            dniEmpleadoActual = dniEmpleado;
            dgvCarrito.DataSource = carrito;
            btnConfirmar.Enabled = false;
        }

        private void FormVenta_Load(object sender, EventArgs e)
        {
            try
            {
                LanguageManager.Instance.AgregarObservador(this);
                Actualizar(LanguageManager.Instance);
                catalogo.Clear();
                catalogo.AddRange(ventaBLL.ObtenerCatalogo());
                dgvCatalogo.DataSource = catalogo;
                cmbMedioPago.SelectedIndex = 0;
                cmbDescuento.SelectedIndex = 0;
                cmbAutor.Items.AddRange(libroBLL.ListarAutores());
                cmbCategoria.Items.AddRange(libroBLL.ListarCategorias());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Catálogo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvCatalogo.CurrentRow == null) return;
            var libro = (Libro)dgvCatalogo.CurrentRow.DataBoundItem;
            int cantidad = (int)nudCantidad.Value;

            try
            {
                ventaBLL.ValidarAgregarAlCarrito(libro.CodigoInterno, cantidad);

                var existente = carrito.FirstOrDefault(d => d.CodigoLibro == libro.CodigoInterno);
                if (existente != null)
                {
                    ventaBLL.ValidarAgregarAlCarrito(libro.CodigoInterno, existente.Cantidad + cantidad);
                    existente.Cantidad += cantidad;
                }
                else
                {
                    carrito.Add(new DetalleVenta
                    {
                        CodigoLibro = libro.CodigoInterno,
                        TituloLibro = libro.Titulo,
                        Cantidad = cantidad,
                        PrecioUnitario = libro.Precio
                    });
                }

                btnConfirmar.Enabled = true;
                dgvCarrito.DataSource = carrito;
                dgvCarrito.Refresh();
                RecalcularTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvCarrito.CurrentRow == null)
            {
                btnConfirmar.Enabled = false;
                return;
            }
                
            carrito.Remove((DetalleVenta)dgvCarrito.CurrentRow.DataBoundItem);
            RecalcularTotales();
        }

        private void cmbMedioPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalcularTotales();
        }

        private void RecalcularTotales()
        {
            decimal subtotal = carrito.Sum(d => d.Subtotal);
            string medioPago = cmbMedioPago.SelectedItem?.ToString();

            int promoAplicada = int.TryParse(cmbDescuento.Text, out int resultado) ? resultado : 0;

            decimal total = ventaBLL.CalcularTotal(carrito.ToList(), medioPago, promoAplicada, out decimal descuento);

            lblSubtotal.Text = $"{LanguageManager.Instance.GetTraduction("lblSubtotal")}: ${subtotal:0.00}";
            lblDescuento.Text = $"{LanguageManager.Instance.GetTraduction("lblDescuento")}: -${descuento:0.00}";
            lblTotal.Text = $"{LanguageManager.Instance.GetTraduction("lblTotal")}: ${total:0.00}";
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (carrito.Count == 0)
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("msjErrorVentaCarrito"), LanguageManager.Instance.GetTraduction("Venta"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var selectorClientes = new formCarritoClientes())
            {
                if (selectorClientes.ShowDialog(this) != DialogResult.OK)
                    return;

                dniClienteSeleccionado = selectorClientes.DniSeleccionado;
            }

            int promoAplicada = int.TryParse(cmbDescuento.Text, out int resultado) ? resultado : 0;
            decimal subtotal = carrito.Sum(d => d.Subtotal);
            decimal total = ventaBLL.CalcularTotal(carrito.ToList(), cmbMedioPago.SelectedItem?.ToString(), promoAplicada, out decimal descuento);

            string usuario = SessionManager.Instance.UsuarioActual().DNI;

            var venta = new Venta
            {
                Fecha = DateTime.Now,
                DniUsuario = usuario,
                MedioDePago = cmbMedioPago.SelectedItem?.ToString(),   
                Subtotal = subtotal,
                Descuento = descuento,
                DniCliente = dniClienteSeleccionado,
                Detalle = carrito.ToList()
            };
            try
            {
                ventaBLL.ConfirmarVenta(venta);
                MessageBox.Show(LanguageManager.Instance.GetTraduction("msjVentaExitosa"), LanguageManager.Instance.GetTraduction("Venta"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Archivo PDF (*.pdf)|*.pdf",
                    FileName = $"Comprobante_Venta_{venta.NroVenta}.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Generar el documento
                    ComprobanteService.GenerarComprobantePDF(venta, saveFileDialog.FileName);

                    MessageBox.Show(LanguageManager.Instance.GetTraduction("msjComprobanteExitoso"), LanguageManager.Instance.GetTraduction("Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir el PDF automáticamente en el visor de PDF por defecto de Windows
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
                
                dgvCarrito.DataSource = null;
                carrito.Clear();
                catalogo.Clear();
                catalogo.AddRange(ventaBLL.ObtenerCatalogo());
                dgvCatalogo.DataSource = catalogo;
                cmbMedioPago.SelectedIndex = 0;

                btnComprobante.Enabled = true;
                btnConfirmar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("msjErrorVenta"),
                    LanguageManager.Instance.GetTraduction("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // CU-005: Generar Comprobante
        private void btnComprobante_Click(object sender, EventArgs e)
        {
        }

        private void btnCerrar_Click(object sender, EventArgs e) => this.Close();
        private void btnMinimizar_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu menu = new Menu();
            menu.Show();
        }

        private void btnAplicarFiltro_Click(object sender, EventArgs e)
        {
            catalogo.Clear();


            catalogo.AddRange(ventaBLL.FiltrarCatalogo(cmbCategoria.Text, cmbAutor.Text, txtTitulo.Text));

            if (catalogo.Count == 0)
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("msjErrorFiltros"), LanguageManager.Instance.GetTraduction("Error"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dgvCatalogo.DataSource = null;
            dgvCatalogo.DataSource = catalogo;
            dgvCatalogo.ClearSelection();
            dgvCatalogo.Refresh();
            cmbMedioPago.SelectedIndex = 0;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbAutor.Text= "";
            cmbCategoria.Text= "";
            txtTitulo.Text= "";

        }

        public void Actualizar(LanguageManager lenguaje)
        {
            lblTituloVenta.Text = LanguageManager.Instance.GetTraduction("lblTituloVenta");
            lblTituloLibro.Text = LanguageManager.Instance.GetTraduction("lblTituloLibro");
            lblAutorLibro.Text = LanguageManager.Instance.GetTraduction("lblAutorLibro");

            lblCategoriaLibro.Text = LanguageManager.Instance.GetTraduction("lblCategoriaLibro");
            lblCantidadVenta.Text = LanguageManager.Instance.GetTraduction("lblCantidadVenta");
            lblMedioDePago.Text = LanguageManager.Instance.GetTraduction("lblMedioDePago");

            lblSubtotal.Text = LanguageManager.Instance.GetTraduction("lblSubtotal");
            lblDescuento.Text = LanguageManager.Instance.GetTraduction("lblDescuento");

            lblTotal.Text = LanguageManager.Instance.GetTraduction("lblTotal");

            btnAgregar.Text = LanguageManager.Instance.GetTraduction("btnAgregar");
            btnQuitar.Text = LanguageManager.Instance.GetTraduction("btnQuitar");

            btnComprobante.Text = LanguageManager.Instance.GetTraduction("btnComprobante");
            btnVolver.Text = LanguageManager.Instance.GetTraduction("btnVolver");

            btnAplicarFiltro.Text = LanguageManager.Instance.GetTraduction("btnAplicarFiltro");
            btnConfirmar.Text = LanguageManager.Instance.GetTraduction("btnConfirmar");
        }

        private void cmbDescuento_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalcularTotales();
        }
    }
}