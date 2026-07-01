using BLL;
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
    public partial class frmReparacionDV : Form
    {
        DigitoVerificadorBLL DVBLL;
        RespaldoBLL backupBLL;
        public frmReparacionDV()
        {
            InitializeComponent();
            
        }

        private void btnRecalcularDV_Click(object sender, EventArgs e)
        {
            if(Convert.ToBoolean(MessageBox.Show("Se recalcularan los digitos verificadores de todas las tablas, esta seguro?", "Recalculo de DV", MessageBoxButtons.YesNo, MessageBoxIcon.Information)))
            {

                DVBLL.RecalcularDVV_General();
                MessageBox.Show("Se recalcularon los digitos verificadores correctamente", "Recalculo de DV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                Login login = new Login();
                login.Show();
            }
            
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstBackups.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un backup para restaurar", "Restauracion de backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                backupBLL.RealizarRestore(backupBLL.ObtenerRutaBakcup(lstBackups.SelectedItem.ToString()));
                MessageBox.Show("Se realizo la restauracion del backup correctamente", "Restauracion de backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                Login login = new Login();
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al restaurar el backup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void frmReparacionDV_Load(object sender, EventArgs e)
        {
            DVBLL = new DigitoVerificadorBLL();
            backupBLL = new RespaldoBLL();
            listarIncocistencias();
            listarBackups();
        }
        private void listarIncocistencias()
        {
            lstInconsistencias.Items.Clear();
            List<string> auditoria = DVBLL.EjecutarAuditoriaDetalladaCompleta();

            foreach (string inconsistencia in auditoria)
            {
                lstInconsistencias.Items.Add(inconsistencia);
            }

        }
        private void listarBackups()
        {
            try
            {
                lstBackups.Items.Clear();

                List<string> listaDeBackups = backupBLL.ObtenerListaBackups();

                if (listaDeBackups.Count == 0)
                {
                    MessageBox.Show("No se encontraron archivos de respaldo (.bak) en la carpeta del programa.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (string backup in listaDeBackups)
                {
                    lstBackups.Items.Add(backup);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar los respaldos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Esta seguro que desea cerrar la ventana de reparacion de digitos verificadores?", "Cerrar ventana", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                Login login = new Login();
                login.Show();
            }
        }
    }
}
