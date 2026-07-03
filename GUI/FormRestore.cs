using BLL;
using Servicios;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormRestore : Form,IObserver
    {
        int posX, posY;
        bool arrastrando = false;
        RespaldoBLL bll;

        public FormRestore()
        {
            InitializeComponent();
            bll = new RespaldoBLL();
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Title = "Seleccione el archivo de Backup a restaurar";
                dialogo.Filter = "Archivos de Backup (*.bak)|*.bak|Todos los archivos (*.*)|*.*";
                if (dialogo.ShowDialog() == DialogResult.OK)
                    txtPath.Text = dialogo.FileName;
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPath.Text))
                {
                    MessageBox.Show("Debe seleccionar un archivo de Backup para restaurar.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    "Esta operación reemplazará la base de datos actual.\n" +
                    "Archivo: " + Path.GetFileName(txtPath.Text) +
                    "\n\n¿Desea continuar?",
                    "Confirmar Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes)
                    return;

                this.Cursor = Cursors.WaitCursor;
                btnEjecutar.Enabled = false;

                bll.RealizarRestore(txtPath.Text);

                MessageBox.Show("Restore ejecutado correctamente.",
                    "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo ejecutar el Restore.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnEjecutar.Enabled = true;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.Show();
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

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                arrastrando = true;
                posX = e.X;
                posY = e.Y;
            }
        }
        private void BarraTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            arrastrando = false;
        }

        private void BarraTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastrando)
            {
                this.Location = new Point(this.Location.X + (e.X - posX), this.Location.Y + (e.Y - posY));
            }
        }

        private void FormRestore_Load(object sender, EventArgs e)
        {
            LanguageManager.Instance.AgregarObservador(this);
            Actualizar(LanguageManager.Instance);
        }

        public void Actualizar(LanguageManager lenguaje)
        {
            lblTituloRes.Text = lenguaje.GetTraduction("lblTituloRes");
            lblPath.Text = lenguaje.GetTraduction("lblPath");
            btnExaminar.Text = lenguaje.GetTraduction("btnExaminar");
            btnEjecutar.Text = lenguaje.GetTraduction("btnEjecutar");
            btnVolver.Text = lenguaje.GetTraduction("btnVolverDV");

        }
    }
}