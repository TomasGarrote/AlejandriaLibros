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

        }

        private void frmReparacionDV_Load(object sender, EventArgs e)
        {
            DVBLL = new DigitoVerificadorBLL();
            lstInconsistencias.Items.Clear();
            List<string> auditoria = DVBLL.EjecutarAuditoriaDetalladaCompleta();

            foreach (string inconsistencia in auditoria)
            {
                lstInconsistencias.Items.Add(inconsistencia);
            }
        }
        private void listarIncocistencias()
        {
            lstInconsistencias.Items.Clear();

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
