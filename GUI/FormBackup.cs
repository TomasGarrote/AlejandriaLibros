using BLL;
using Servicios;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormBackup : Form
    {
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
    }
}