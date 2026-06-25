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
    public partial class Usuarios : Form,IObserver
    {
        int posX, posY;
        bool arrastrando = false;
        private UserAction userAction;
        private UsuarioBLL usuarioBLL;
        public Usuarios()
        {
            
            InitializeComponent();
            usuarioBLL = new UsuarioBLL();
            lblTextoTabla.Text = LanguageManager.Instance.GetTraduction("lblTextoTablaUserActivos");
            MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
            MostrarCantidadUsuarios();
            ConfigurarGrillaSeleccionFila(dataGridView1);
            ReiniciarBotones();
        }

        private void MostrarCantidadUsuarios()
        {
            if (rbActivos.Checked)
            {
                lblCantidadUsers.Text = $"{LanguageManager.Instance.GetTraduction("lblCantidadUser")}: {usuarioBLL.ListarUsuariosActivos().Count}";
            }
            else
            {
                lblCantidadUsers.Text = $"{LanguageManager.Instance.GetTraduction("lblCantidadUser")}: {usuarioBLL.ListarTodosUsuarios().Count}";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbActivos.Checked)
            {
                lblTextoTabla.Text = LanguageManager.Instance.GetTraduction("lblTextoTablaUserActivos");
                MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos() as List<Usuario>);
                MostrarCantidadUsuarios();
            }
            else
            {
                lblTextoTabla.Text = LanguageManager.Instance.GetTraduction("lblTextoTablaUserTodos");
                MostrarCantidadUsuarios();
            }

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
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
                
        private void EnabledControls(params Control[] controls)
        {
            foreach (Control b in controls)
            {
                b.Enabled = !b.Enabled;
                if (b.Name == "btnAplicar" || b.Name == "btnCancelar")
                {
                    b.BackColor = Color.Green;
                    panel10.BackColor = Color.Green;
                    panel11.BackColor = Color.Green;
                }
                else
                {
                    b.BackColor = Color.Maroon;
                }
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
                            usuarioBLL.RegistrarUsuario(new Usuario(txtDni.Text,txtNom.Text,txtApe.Text,txtUsuario.Text,Encriptador.GetHash256(txtDni.Text + txtNom.Text), txtEmail.Text,false,true,txtRol.Text));
                            ReiniciarBotones();
                            MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
                            MostrarCantidadUsuarios();
                        }
                            break;
                    case UserAction.Delete:
                        usuarioBLL.EliminarLogico((dataGridView1.SelectedRows[0].DataBoundItem as Usuario).DNI);
                        ReiniciarBotones();
                        if (rbActivos.Checked)
                        {
                            MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
                        }
                        else
                        {
                            MostrarUsuarios(dataGridView1, usuarioBLL.ListarTodosUsuarios());

                        }

                        MostrarCantidadUsuarios();

                        MessageBox.Show(LanguageManager.Instance.GetTraduction("UserElimi"), LanguageManager.Instance.GetTraduction("Alerta"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case UserAction.Modify:
                        txtDni.ReadOnly = true;
                        if (ValidarCamposVacios(txtNom, txtApe, txtDni, txtUsuario, txtEmail, txtRol) && ValidarEntradaUsuario())
                        {
                            usuarioBLL.Modificar((dataGridView1.SelectedRows[0].DataBoundItem as Usuario).DNI, new Usuario(txtDni.Text, txtNom.Text, txtApe.Text, txtUsuario.Text, string.Empty, txtEmail.Text, default, default, txtRol.Text));
                            txtDni.ReadOnly = false;
                            ReiniciarBotones();
                            MessageBox.Show(LanguageManager.Instance.GetTraduction("UserModif"), LanguageManager.Instance.GetTraduction("Alerta") , MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
                        }
                        break;
                    case UserAction.UnBlock:
                        ReiniciarBotones();
                        usuarioBLL.DesbloquearUsuario(dataGridView1.SelectedRows[0].DataBoundItem as Usuario);
                        if (rbActivos.Checked)
                        {
                            MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
                        }
                        else
                        {
                            MostrarUsuarios(dataGridView1, usuarioBLL.ListarTodosUsuarios());

                        }
                        
                        MessageBox.Show(LanguageManager.Instance.GetTraduction("UserDesbloqueado"), LanguageManager.Instance.GetTraduction("Alerta"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case UserAction.Activate:
                        usuarioBLL.EliminarLogico((dataGridView1.SelectedRows[0].DataBoundItem as Usuario).DNI);
                        ReiniciarBotones();
                        if (rbActivos.Checked)
                        {
                            MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
                        }
                        else
                        {
                            MostrarUsuarios(dataGridView1, usuarioBLL.ListarTodosUsuarios());

                        }
                        MessageBox.Show(LanguageManager.Instance.GetTraduction("UserActi"), LanguageManager.Instance.GetTraduction("Alerta"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        public void MostrarUsuarios(DataGridView dgv, object obj) { dgv.DataSource = null; dgv.DataSource = obj; }
            

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
                    mensaje.AppendLine($"{LanguageManager.Instance.GetTraduction("ElCampo")} " + " " + nombreCampo + " " +  LanguageManager.Instance.GetTraduction("EstaVacio"));
                    if (primerInvalido == null) primerInvalido = txt;
                    hayVacios = true;
                }
                else if (c is ComboBox cb && cb.SelectedIndex == -1)
                {
                    mensaje.AppendLine($"{LanguageManager.Instance.GetTraduction("ElCampo")}" + " " + nombreCampo+ " " + LanguageManager.Instance.GetTraduction("NoFueSelecc"));
                    if (primerInvalido == null) primerInvalido = cb;
                    hayVacios = true;
                }
            }

            if (hayVacios)
            {
                MessageBox.Show(mensaje.ToString(), LanguageManager.Instance.GetTraduction("FaltaCompl"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                primerInvalido?.Focus(); // Enfocar el primer campo con error
            }

            return !hayVacios; // true si está todo bien
        }

        private bool ValidarEntradaUsuario()
        {
            StringBuilder errores = new StringBuilder();

           
            if (!Regex.IsMatch(txtNom.Text.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$"))
                errores.AppendLine(LanguageManager.Instance.GetTraduction("CampoNombre"));

            if (!Regex.IsMatch(txtApe.Text.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$"))
                errores.AppendLine(LanguageManager.Instance.GetTraduction("CampoApell"));

            
            if (!Regex.IsMatch(txtDni.Text.Trim(), @"^\d{8}$"))
                errores.AppendLine(LanguageManager.Instance.GetTraduction("CampoDni"));

            if (!(userAction == UserAction.Add))
            {
                
                if (string.IsNullOrWhiteSpace(txtUsuario.Text) || txtUsuario.Text.Length < 4)
                    errores.AppendLine(LanguageManager.Instance.GetTraduction("ElNombreUser"));
            }
           
            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errores.AppendLine(LanguageManager.Instance.GetTraduction("ElCorreoElec"));

           
            if (string.IsNullOrWhiteSpace(txtRol.Text))
                errores.AppendLine(LanguageManager.Instance.GetTraduction("DebeSelecionar"));

            
            if (errores.Length > 0)
            {
                MessageBox.Show(errores.ToString(), LanguageManager.Instance.GetTraduction("ErrorEntradaDeDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            userAction = UserAction.Modify;
            EnabledControls(btnCrear, btnDesbloquear, btnModificar, btnActDes, btnAplicar, btnCancelar, btnSalir, pnFiltrado, panModificarUsuario,txtDni);
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

            pnFiltrado.Enabled = true;
            panel3.Enabled = true;
            panModificarUsuario.Enabled = false;

            btnAplicar.Enabled = false;
            btnCancelar.Enabled = false;
            btnAplicar.BackColor = Color.Maroon;
            btnCancelar.BackColor = Color.Maroon;

            panel10.BackColor = Color.Maroon;
            panel11.BackColor = Color.Maroon;

            btnCrear.Enabled = true;
            btnDesbloquear.Enabled = true;
            btnModificar.Enabled = true;
            btnActDes.Enabled = true;
            btnCrear.BackColor = Color.Green;
            btnDesbloquear.BackColor = Color.Green;
            btnModificar.BackColor = Color.Green;
            btnActDes.BackColor = Color.Green;

            //txtNom.Enabled = false;
            //txtApe.Enabled = false;
            //txtDni.Enabled = false;
            //txtEmail.Enabled = false;
            //txtRol.Enabled = false;
            //txtUsuario.Enabled = false;

            txtBoxModo.Text = LanguageManager.Instance.GetTraduction("txtBoxModoConsulta");

            userAction = UserAction.Consult;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            MostrarUsuarios(dataGridView1, usuarioBLL.ListarTodosUsuarios() as List<Usuario>);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (userAction == UserAction.Delete || userAction == UserAction.Modify || userAction == UserAction.UnBlock)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    Usuario user = usuarioBLL.BuscarUsuarioPorDNI(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    txtDni.Text = user.DNI;
                    txtNom.Text = user.Nombre;
                    txtApe.Text = user.Apellido;
                    txtUsuario.Text = user.Username;
                    txtEmail.Text = user.Email;
                    txtRol.Text = user.Rol;
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].DataBoundItem is Usuario usuario)
            { 
                if (usuario.Bloqueado)
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            userAction = UserAction.Add;
            txtBoxModo.Text = LanguageManager.Instance.GetTraduction("txtBoxModoAnadir");
            EnabledControls(btnCrear, btnDesbloquear, btnModificar, btnActDes, btnAplicar, btnCancelar, btnSalir, pnFiltrado, panModificarUsuario, panel3);
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                userAction = UserAction.UnBlock;
                txtBoxModo.Text = LanguageManager.Instance.GetTraduction("txtBoxModoDesbloquear");
                EnabledControls(btnCrear, btnDesbloquear, btnModificar, btnActDes, btnAplicar, btnCancelar, btnSalir, pnFiltrado);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            userAction = UserAction.Modify;
            EnabledControls(btnCrear, btnDesbloquear, btnModificar, btnActDes, btnAplicar, btnCancelar, btnSalir, pnFiltrado, panModificarUsuario, txtDni);
            MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
        }

        private void btnActDes_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Usuario us = usuarioBLL.BuscarUsuarioPorDNI(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                if (!us.Activo)
                {
                    userAction = UserAction.Activate;
                    txtBoxModo.Text = LanguageManager.Instance.GetTraduction("txtBoxModoActivar");
                }
                else
                {
                    userAction = UserAction.Delete;
                    txtBoxModo.Text = LanguageManager.Instance.GetTraduction("txtBoxModoEliminar");
                }
                EnabledControls(btnCrear, btnDesbloquear, btnModificar, btnActDes, btnAplicar, btnCancelar, btnSalir, pnFiltrado, panModificarUsuario);
            }
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            LanguageManager.Instance.AgregarObservador(this);
            Actualizar(LanguageManager.Instance);
        }

        private void ConfigurarGrillaSeleccionFila(DataGridView dgv)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
        }

        public void Actualizar(LanguageManager lenguaje)
        {

            lblTextoTabla.Text = lenguaje.GetTraduction("lblTextoTabla");
            //lblCantidadUsers.Text = lenguaje.GetTraduction("lblCantidadUsers");
            lblApe.Text = lenguaje.GetTraduction("lblApe");
            lblNombre.Text = lenguaje.GetTraduction("lblNombre");
            lblEmail.Text = lenguaje.GetTraduction("lblEmail");
            lblRol.Text = lenguaje.GetTraduction("lblRol");
            lblUsuarioFU.Text = lenguaje.GetTraduction("lblUsuarioFU");
            lblMsj.Text = lenguaje.GetTraduction("lblMsj");
            lblOpciones.Text = lenguaje.GetTraduction("lblOpciones");

            btnCrear.Text = lenguaje.GetTraduction("btnCrear");
            btnDesbloquear.Text = lenguaje.GetTraduction("btnDesbloquear");
            btnModificar.Text = lenguaje.GetTraduction("btnModificar");
            btnActDes.Text = lenguaje.GetTraduction("btnActDes");
            btnAplicar.Text = lenguaje.GetTraduction("btnAplicar");
            btnCancelar.Text = lenguaje.GetTraduction("btnCancelar");
            btnSalir.Text = lenguaje.GetTraduction("btnSalir");

            rbTodos.Text = lenguaje.GetTraduction("rbTodos");
            rbActivos.Text = lenguaje.GetTraduction("rbActivos");
        }
    }
}
