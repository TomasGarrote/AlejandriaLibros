using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Usuarios : Form
    {
        int posX, posY;
        bool arrastrando = false;
        private UserAction userAction;
        private UsuarioBLL usuarioBLL;
        public Usuarios()
        {
            InitializeComponent();
            usuarioBLL = new UsuarioBLL();
            lblTextoTabla.Text = $"[Usuarios Activos] + {usuarioBLL.ListarUsuariosActivos().Count}";
            MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
            ConfigurarGrillaSeleccionFila(dataGridView1);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                lblTextoTabla.Text = "[Usuarios Activos]";
                MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos() as List<UsuarioBE>);
            }
            else
            {
                lblTextoTabla.Text = "[Todos los Usuarios]";
            }

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu menu = new Menu();
            menu.Show();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            arrastrando = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastrando)
            {
                this.Location = new Point(this.Location.X + (e.X - posX), this.Location.Y + (e.Y - posY));
            }
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

        private void panModificarUsuario_EnabledChanged(object sender, EventArgs e)
        {
            if (panModificarUsuario.Enabled == true)
            {
                txtDni.BackColor = Color.White;
                txtNom.BackColor = Color.White;
                txtApe.BackColor =  Color.White;
                txtEmail.BackColor = Color.White;
                txtUsuario.BackColor = Color.White;
                txtRol.BackColor = Color.White;
            }
            else
            {
                txtDni.BackColor = Color.Gray;
                txtNom.BackColor = Color.Gray;
                txtApe.BackColor =  Color.Gray;
                txtEmail.BackColor = Color.Gray;
                txtUsuario.BackColor = Color.Gray;
                txtRol.BackColor = Color.Gray;
            }
        }
                
        //Crear Usuario
        private void button1_Click(object sender, EventArgs e)
        {
            userAction = UserAction.Add;
            textBox1.Text = "Modo Añadir";
            EnabledControls(button1, button2, button3, button4, btnAplicar, btnCancelar, button7, panel2, panModificarUsuario, panel3);
            
        }
        private void EnabledControls(params Control[] controls)
        {
            foreach (Control b in controls)
            {
                b.Enabled = !b.Enabled;
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (userAction)
                {
                    case UserAction.Add:
                        if (ValidarCamposVacios(txtDni,txtApe,txtNom,txtEmail,txtRol,txtUsuario)) { 
                        usuarioBLL.RegistrarUsuario(new UsuarioBE(txtDni.Text,txtNom.Text,txtApe.Text,txtUsuario.Text,Encriptador.GetHash256(txtDni.Text + txtNom.Text), txtEmail.Text,false,true,txtRol.Text));
                        ReiniciarBotones();
                        MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
                            lblTextoTabla.Text = $"[Usuarios Activos] + {usuarioBLL.ListarUsuariosActivos().Count}";
                        }
                            break;
                    case UserAction.Delete:
                        usuarioBLL.EliminarLogico((dataGridView1.SelectedRows[0].DataBoundItem as UsuarioBE).DNI);
                        ReiniciarBotones();
                        MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
                        break;
                    case UserAction.Modify:
                        if (ValidarCamposVacios(txtNom, txtApe, txtDni, txtUsuario, txtEmail, txtRol) && ValidarEntradaUsuario())
                        {
                            usuarioBLL.Modificar((dataGridView1.SelectedRows[0].DataBoundItem as UsuarioBE).DNI, new UsuarioBE(txtDni.Text, txtNom.Text, txtApe.Text, txtUsuario.Text, string.Empty, txtEmail.Text, default, default, txtRol.Text));
                            ReiniciarBotones();
                            MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
                        }
                        break;
                    case UserAction.UnBlock:
                        usuarioBLL.DesbloquearUsuario(dataGridView1.SelectedRows[0].DataBoundItem as UsuarioBE);
                        ReiniciarBotones();
                        MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
                        break;
                    case UserAction.Consult:
                        break;
                    default:
                        break;

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message); 
            }
        }
        public void MostrarUsuarios(DataGridView dgv, object obj)
        {
            dgv.DataSource = null;
            dgv.DataSource = obj;
            if (obj is List<UsuarioBE> lista)
            {
                for (int i = 0; i < lista.Count && i < dgv.Rows.Count; i++)
                {
                    if (lista[i].Bloqueado)
                    {
                        dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    }
                    else
                    {
                        dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }
        private bool ValidarCamposVacios(params Control[] controles)
        {
            StringBuilder mensaje = new StringBuilder();
            bool hayVacios = false;
            Control primerInvalido = null;

            foreach (Control c in controles)
            {
                string nombreCampo = c.Tag?.ToString() ?? c.Name;

                if (c is TextBox txt && string.IsNullOrWhiteSpace(txt.Text))
                {
                    mensaje.AppendLine("El Campo " + " " + nombreCampo + " " + "Esta Vacio");
                    if (primerInvalido == null) primerInvalido = txt;
                    hayVacios = true;
                }
                else if (c is ComboBox cb && cb.SelectedIndex == -1)
                {
                    mensaje.AppendLine("El Campo" + " " + nombreCampo+ " " + "No Fue Seleccionado");
                    if (primerInvalido == null) primerInvalido = cb;
                    hayVacios = true;
                }
            }

            if (hayVacios)
            {
                MessageBox.Show(mensaje.ToString(), "Faltan Completar Campo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                primerInvalido?.Focus(); // Enfocar el primer campo con error
            }

            return !hayVacios; // true si está todo bien
        }

        private bool ValidarEntradaUsuario()
        {
            StringBuilder errores = new StringBuilder();

           
            if (!Regex.IsMatch(txtNom.Text.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$"))
                errores.AppendLine("El Campo Nombre");

            if (!Regex.IsMatch(txtApe.Text.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$"))
                errores.AppendLine("E lCampo Apellido");

            
            if (!Regex.IsMatch(txtDni.Text.Trim(), @"^\d{8}$"))
                errores.AppendLine("El Campo DNI");

            if (!(userAction == UserAction.Add))
            {
                
                if (string.IsNullOrWhiteSpace(txtUsuario.Text) || txtUsuario.Text.Length < 4)
                    errores.AppendLine("El Nombre Usuario");
            }
           
            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errores.AppendLine("El Correo Electronico");

           
            if (string.IsNullOrWhiteSpace(txtRol.Text))
                errores.AppendLine("Debe Seleccionar");

            
            if (errores.Length > 0)
            {
                MessageBox.Show(errores.ToString(), "Error Entrada Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            userAction = UserAction.Modify;
            EnabledControls(button1, button2, button3, button4, btnAplicar, btnCancelar, button7, panel2, panModificarUsuario);
            MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
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

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                userAction = UserAction.UnBlock;
                textBox1.Text = "Modo desbloquear";
                EnabledControls(button1, button2, button3, button4, btnAplicar, btnCancelar, button7, panel2, panModificarUsuario);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                userAction = UserAction.Delete;
                textBox1.Text = "Modo eliminar";
                EnabledControls(button1, button2, button3, button4, btnAplicar, btnCancelar, button7, panel2, panModificarUsuario);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ReiniciarBotones();
        }

        private void ReiniciarBotones()
        {
            txtNom.Clear();
            txtApe.Clear();
            txtDni.Clear();
            txtEmail.Clear();
            txtRol.Clear();
            txtUsuario.Clear();

            panel2.Enabled = true;
            panel3.Enabled = true;
            panModificarUsuario.Enabled = false;

            btnAplicar.Enabled = false;
            btnCancelar.Enabled = false;

            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;

            //txtNom.Enabled = false;
            //txtApe.Enabled = false;
            //txtDni.Enabled = false;
            //txtEmail.Enabled = false;
            //txtRol.Enabled = false;
            //txtUsuario.Enabled = false;

            textBox1.Text = "Modo consulta";

            userAction = UserAction.Consult;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            MostrarUsuarios(dataGridView1, usuarioBLL.ListarTodosUsuarios() as List<UsuarioBE>);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (userAction == UserAction.Delete || userAction == UserAction.Modify || userAction == UserAction.UnBlock)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    UsuarioBE user = usuarioBLL.BuscarUsuarioPorDNI(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    txtDni.Text = user.DNI;
                    txtNom.Text = user.Nombre;
                    txtApe.Text = user.Apellido;
                    txtUsuario.Text = user.Username;
                    txtEmail.Text = user.Email;
                    txtRol.Text = user.Rol;
                }
            }
        }

        private void ConfigurarGrillaSeleccionFila(DataGridView dgv)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
        }
    }
}
