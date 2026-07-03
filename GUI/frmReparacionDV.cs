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
    public partial class frmReparacionDV : Form,IObserver
    {
        DigitoVerificadorBLL DVBLL;
        RespaldoBLL backupBLL;
        public frmReparacionDV()
        {
            InitializeComponent();
            
        }

        private void btnRecalcularDV_Click(object sender, EventArgs e)
        {
            if(Convert.ToBoolean(MessageBox.Show(LanguageManager.Instance.GetTraduction("TextDV1"), LanguageManager.Instance.GetTraduction("TextDV2"), MessageBoxButtons.YesNo, MessageBoxIcon.Information)))
            {

                DVBLL.RecalcularDVV_General();
                SessionManager.Instance.Desloguear();
                MessageBox.Show(LanguageManager.Instance.GetTraduction("TextDV3"), LanguageManager.Instance.GetTraduction("TextDV2"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show(LanguageManager.Instance.GetTraduction("TextDV4"), LanguageManager.Instance.GetTraduction("TextDV5"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                backupBLL.RealizarRestore(backupBLL.ObtenerRutaBakcup(lstBackups.SelectedItem.ToString()));
                MessageBox.Show(LanguageManager.Instance.GetTraduction("TextDV6"), LanguageManager.Instance.GetTraduction("TextDV7"), MessageBoxButtons.OK, MessageBoxIcon.Information);


                SessionManager.Instance.Desloguear();
                this.Close();
                Login login = new Login();
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{LanguageManager.Instance.GetTraduction("TextDV8")} {ex.Message}", LanguageManager.Instance.GetTraduction("ErrorP"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void frmReparacionDV_Load(object sender, EventArgs e)
        {
            DVBLL = new DigitoVerificadorBLL();
            backupBLL = new RespaldoBLL();
            listarIncocistencias();
            listarBackups();
            LanguageManager.Instance.AgregarObservador(this);
            Actualizar(LanguageManager.Instance);
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
                    MessageBox.Show(LanguageManager.Instance.GetTraduction("TextDV9"), LanguageManager.Instance.GetTraduction("Informacion"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (string backup in listaDeBackups)
                {
                    lstBackups.Items.Add(backup);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{LanguageManager.Instance.GetTraduction("TextDV10")} {ex.Message}", LanguageManager.Instance.GetTraduction("ErrorP"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(LanguageManager.Instance.GetTraduction("TextDV11"), LanguageManager.Instance.GetTraduction("TextDV12"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SessionManager.Instance.Desloguear();
                this.Close();
                Login login = new Login();
                login.Show();
            }
        }

        public void Actualizar(LanguageManager lenguaje)
        {
            lblErrorDV.Text = lenguaje.GetTraduction("lblErrorDV");
            lblModuloReparacion.Text = lenguaje.GetTraduction("lblModuloReparacion");
            lblForzar.Text = lenguaje.GetTraduction("lblForzar");
            btnRecalcularDV.Text = lenguaje.GetTraduction("btnRecalcularDV");
            btnRestaurar.Text = lenguaje.GetTraduction("lblSelecVers");
            lblSelecVers.Text= lenguaje.GetTraduction("btnRestaurar");
            btnCancelardv.Text = lenguaje.GetTraduction("btnCancelardv");
        }
    }
}
