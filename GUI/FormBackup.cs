using BLL;
using Servicios;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormBackup : Form
    {
        int posX, posY;
        bool arrastrando = false;
        RespaldoBLL bll;

        public FormBackup()
        {
            InitializeComponent();
            bll = new RespaldoBLL();
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialogo = new FolderBrowserDialog())
            {
                dialogo.Description = "Seleccione la carpeta donde se guardará el Backup";
                if (dialogo.ShowDialog() == DialogResult.OK)
                    txtPath.Text = dialogo.SelectedPath;
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPath.Text))
                {
                    MessageBox.Show("Debe seleccionar una carpeta de destino.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                btnGenerar.Enabled = false;

                string rutaGenerada = bll.RealizarBackup(txtPath.Text);

                MessageBox.Show("Backup generado correctamente en:\n" + rutaGenerada,
                    "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo generar el Backup.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnGenerar.Enabled = true;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.Show();
        }

        private void FormBackup_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
    }
}