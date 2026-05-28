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

            List<Bitacora> listaInicial = bll.ListarEventos(); 
            dtgvBitacora.DataSource = listaInicial;
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
            if (!ValidarFiltros())
            {
                return;
            }

            string nombre = string.IsNullOrWhiteSpace(txtNombre.Text) ? null : txtNombre.Text.Trim();
            string apellido = string.IsNullOrWhiteSpace(txtApellido.Text) ? null : txtApellido.Text.Trim();
            string login = string.IsNullOrWhiteSpace(cmbLogin.Text) ? null : cmbLogin.Text.Trim();
            string modulo = cmbModulo.SelectedItem?.ToString();
            string evento = string.IsNullOrWhiteSpace(cmbEvento.Text) ? null : cmbEvento.Text.Trim();

            DateTime desde = dtpFechaInicio.Value;
            DateTime hasta = dtpFechaFinal.Value;

            int? criticidad = null;
            if (!string.IsNullOrEmpty(cmbCriticidad.Text) && cmbCriticidad.Text != "Todos")
            {
                criticidad = Convert.ToInt32(cmbCriticidad.Text);
            }

            List<Bitacora> listaFiltrada = bll.FiltrarEventos(nombre, apellido, login, modulo, evento, desde, hasta, criticidad);

            if (listaFiltrada.Count == 0)
            {
                MessageBox.Show("No hay coincidencias", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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
    }
}
