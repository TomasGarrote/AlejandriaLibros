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
    public partial class CambiarIdioma : Form,IObserver
    {
        int posX, posY;
        bool arrastrando = false;
        public CambiarIdioma()
        {
            InitializeComponent();
        }
        private void IniciarListaDeIdiomas()
        {
            var lista = new List<object>()
            {
                new {Idioma=LanguageManager.Instance.GetTraduction("Español"), Codigo="es"},
                new {Idioma=LanguageManager.Instance.GetTraduction("Ingles"), Codigo="en"},
                new {Idioma=LanguageManager.Instance.GetTraduction("Japones"), Codigo="ja"}
            };

            cbIdiomas.DisplayMember = "Idioma";
            cbIdiomas.ValueMember = "Codigo";
            cbIdiomas.DataSource = lista;

            int ind = 0;
            foreach (object item in cbIdiomas.Items)
            {
                if (item.GetType().GetProperty("Idioma").GetValue(item) as string
                    == LanguageManager.Instance.GetCurrentLanguage().EnglishName)
                {
                    ind = cbIdiomas.Items.IndexOf(item);
                }
            }
            cbIdiomas.SelectedIndex = ind;
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                string idioma =
                    cbIdiomas.SelectedValue.ToString();

                LanguageManager.Instance
                    .CargarIdioma(idioma);

                MessageBox.Show(
                    LanguageManager.Instance.GetTraduction("MsjCambiarIdioma"),
                    LanguageManager.Instance.GetTraduction("MsjConfirmacion"),
                    MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Actualizar(LanguageManager lenguaje)
        {
            try
            {
                btnCambiarCI.Text = lenguaje.GetTraduction("btnCambiarCI");
                lblCambiarIdioma.Text = lenguaje.GetTraduction("lblCambiarIdioma");
                button1.Text = LanguageManager.Instance.GetTraduction("Salir");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CambiarIdioma_Load(object sender, EventArgs e)
        {
            LanguageManager.Instance.AgregarObservador(this);
            LanguageManager.Instance.NotificarObservadores();
            IniciarListaDeIdiomas();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu cambiarContraseña = new Menu();
            cambiarContraseña.Show();
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
        private void BarraTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastrando)
            {
                this.Location = new Point(this.Location.X + (e.X - posX), this.Location.Y + (e.Y - posY));
            }
        }
        private void BarraTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            arrastrando = false;
        }
    }
}

