using BLL;
using Servicios;
using System;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormRestore : Form
    {
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
    }
}