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
    public partial class FormBitacora : Form
    {
        int posX, posY;
        bool arrastrando = false;
        BitacoraBLL bll; 
        public FormBitacora()
        {
          
            InitializeComponent();
            bll = new BitacoraBLL();
            dtpFechaFinal.Value = DateTime.Now;
            dtpFechaInicio.Value = DateTime.Now.AddDays(-3);

            
            cmbLogin.Items.Clear();
            foreach (var l in bll.ObtenerLogins())
                cmbLogin.Items.Add(l);

            CargarFiltros();

            List<Bitacora> listaInicial = bll.ListarEventos();
            dtgvBitacora.DataSource = listaInicial;
            ConfigurarDtgv();

            dtgvBitacora.SelectionChanged += DtgvBitacora_SelectionChanged;

            if (dtgvBitacora.Rows.Count > 0)
                dtgvBitacora.Rows[0].Selected = true;
        
         
        }

        private void CargarFiltros()
        {
            // Logins
            cmbLogin.Items.Clear();
            foreach (var l in bll.ObtenerLogins())
                cmbLogin.Items.Add(l);

            // Módulos
            cmbModulo.Items.Clear();
            cmbModulo.Items.Add("Todos");
            cmbModulo.Items.Add("Usuarios");
            cmbModulo.Items.Add("Ventas");
            cmbModulo.Items.Add("Compras");
            cmbModulo.Items.Add("Maestro");
            cmbModulo.Items.Add("Perfiles");
            cmbModulo.SelectedIndex = 0;

            // Eventos
            cmbEvento.Items.Clear();
            cmbEvento.Items.Add("Login");
            cmbEvento.Items.Add("Logout");
            cmbEvento.Items.Add("Crear Usuario");
            cmbEvento.Items.Add("Cambiar Clave");
            cmbEvento.Items.Add("Bloquear Usuario");
            cmbEvento.Items.Add("Generar Carrito");
            cmbEvento.Items.Add("Generar Factura");
            cmbEvento.Items.Add("Imprimir Factura");
            cmbEvento.Items.Add("Eliminar Producto");

            // Criticidad
            cmbCriticidad.Items.Clear();
            cmbCriticidad.Items.Add("Todos");
            for (int i = 1; i <= 5; i++)
                cmbCriticidad.Items.Add(i.ToString());
            cmbCriticidad.SelectedIndex = 0;
        }

        private void DtgvBitacora_SelectionChanged(object sender, EventArgs e)
        {
            if (dtgvBitacora.CurrentRow?.DataBoundItem is Bitacora b)
            {
                UsuarioBE u = bll.ObtenerUsuarioPorLogin(b.Login);
                if (u != null)
                {
                    txtNombre.Text = u.Nombre;
                    txtApellido.Text = u.Apellido;
                }
            }
        }

        private void ConfigurarDtgv()
        {
            dtgvBitacora.ReadOnly = true;
            dtgvBitacora.AllowUserToAddRows = false;
            dtgvBitacora.AllowUserToDeleteRows = false;
            dtgvBitacora.AllowUserToOrderColumns = false;
            dtgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dtgvBitacora.Columns["Id_Evento"].Visible = false;
            dtgvBitacora.Columns["Descripcion"].HeaderText = "Evento";
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            arrastrando = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                arrastrando = true;
                posX = e.X;
                posY = e.Y;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (!ValidarFiltros()) return;

            string nombre = string.IsNullOrWhiteSpace(txtNombre.Text) ? null : txtNombre.Text.Trim();
            string apellido = string.IsNullOrWhiteSpace(txtApellido.Text) ? null : txtApellido.Text.Trim();
            string login = string.IsNullOrWhiteSpace(cmbLogin.Text) ? null : cmbLogin.Text.Trim();
            string modulo = cmbModulo.SelectedItem?.ToString();
            string evento = string.IsNullOrWhiteSpace(cmbEvento.Text) ? null : cmbEvento.Text.Trim();

            DateTime desde = dtpFechaInicio.Value;
            DateTime hasta = dtpFechaFinal.Value;

            int? criticidad = null;
            if (!string.IsNullOrEmpty(cmbCriticidad.Text) && cmbCriticidad.Text != "Todos")
                criticidad = Convert.ToInt32(cmbCriticidad.Text);

            List<Bitacora> listaFiltrada = bll.FiltrarEventos(nombre, apellido, login, modulo, evento, desde, hasta, criticidad);

            if (listaFiltrada.Count == 0)
                MessageBox.Show("No hay coincidencias", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;

            dtgvBitacora.DataSource = null;
            dtgvBitacora.DataSource = listaFiltrada;

        }

        private bool ValidarFiltros()
        {
            if (dtpFechaInicio.Value > dtpFechaFinal.Value)
            {
                MessageBox.Show("Datos Incorrectos: La fecha inicio es mayor a la final.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastrando)
            {
                this.Location = new Point(this.Location.X + (e.X - posX), this.Location.Y + (e.Y - posY));
            }
        }

        private void cmbLogin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbLogin.Text))
            {
                UsuarioBE u = bll.ObtenerUsuarioPorLogin(cmbLogin.Text);
                if (u != null)
                {
                    txtNombre.Text = u.Nombre;
                    txtApellido.Text = u.Apellido;
                }
            }
            else
            {
                txtNombre.Text = string.Empty;
                txtApellido.Text = string.Empty;
            }
        }
    }
}
