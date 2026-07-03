using Microsoft.Data.SqlClient;
using Servicios;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmConexionInicial : Form
    {
        private const string BASE_DATOS = "Alejandria_DB";

        public FrmConexionInicial()
        {
            InitializeComponent();
        }

        private void FrmConexionInicial_Load(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            cmbServidor.Items.Clear();

            var instancias = SqlInstanceFinder.ObtenerInstancias();
            foreach (var instancia in instancias)
                cmbServidor.Items.Add(instancia);

            if (cmbServidor.Items.Count > 0)
                cmbServidor.SelectedIndex = 0;

            Cursor = Cursors.Default;
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            string servidor = cmbServidor.Text.Trim();

            if (string.IsNullOrEmpty(servidor))
            {
                MessageBox.Show("Seleccioná o escribí un servidor.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string cadenaConexion = $"Data Source={servidor};Initial Catalog={BASE_DATOS};Integrated Security=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                }

                ConexionConfig.GuardarCadenaConexion(cadenaConexion);

                MessageBox.Show("Conexión exitosa. Configuración guardada.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo conectar:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}