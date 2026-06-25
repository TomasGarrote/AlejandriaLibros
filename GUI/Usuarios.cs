using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GUI
{
    public partial class Usuarios : Form, IObserver
    {
        int posX, posY;
        bool arrastrando = false;
        private UserAction userAction;
        private UsuarioBLL usuarioBLL;
        private PerfilBLL perfilBLL;

        public Usuarios()
        {
            InitializeComponent();
            usuarioBLL = new UsuarioBLL();
            perfilBLL = new PerfilBLL();
            lblTextoTabla.Text = "[Usuarios Activos]";
            MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
            MostrarCantidadUsuarios();
            ConfigurarGrillaSeleccionFila(dataGridView1);
            ReiniciarBotones();
        }

        private void CargarPerfiles()
        {
            cbRol.Items.Clear();
            foreach (var perfil in perfilBLL.ObtenerPerfiles())
                cbRol.Items.Add(perfil.Nombre);
        }

        private void MostrarCantidadUsuarios()
        {
            if (rbActivos.Checked)
                lblCantidadUsers.Text = $"Cantidad de Usuarios: {usuarioBLL.ListarUsuariosActivos().Count}";
            else
                lblCantidadUsers.Text = $"Cantidad de Usuarios: {usuarioBLL.ListarTodosUsuarios().Count}";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbActivos.Checked)
            {
                lblTextoTabla.Text = "[Usuarios Activos]";
                MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
                MostrarCantidadUsuarios();
            }
            else
            {
                lblTextoTabla.Text = "[Todos los Usuarios]";
                MostrarCantidadUsuarios();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.Show();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e) => arrastrando = false;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastrando)
                this.Location = new Point(this.Location.X + (e.X - posX), this.Location.Y + (e.Y - posY));
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnMinimizar_Click(object sender, EventArgs e) =>
            this.WindowState = FormWindowState.Minimized;

        private void panModificarUsuario_EnabledChanged(object sender, EventArgs e)
        {
            Color color = panModificarUsuario.Enabled ? Color.White : Color.Gray;
            txtDni.BackColor = color;
            txtNom.BackColor = color;
            txtApe.BackColor = color;
            txtEmail.BackColor = color;
            txtUsuario.BackColor = color;
            cbRol.BackColor = color;
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
                        if (ValidarCamposVacios(txtDni, txtApe, txtNom, txtEmail, txtUsuario) && ValidarComboBox(cbRol))
                        {
                            usuarioBLL.RegistrarUsuario(new Usuario(
                                txtDni.Text, txtNom.Text, txtApe.Text, txtUsuario.Text,
                                Encriptador.GetHash256(txtDni.Text + txtNom.Text),
                                txtEmail.Text, false, true, cbRol.SelectedItem.ToString()));
                            ReiniciarBotones();
                            MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());
                            MostrarCantidadUsuarios();
                        }
                        break;

                    case UserAction.Delete:
                        usuarioBLL.EliminarLogico((dataGridView1.SelectedRows[0].DataBoundItem as Usuario).DNI);
                        ReiniciarBotones();
                        MostrarUsuarios(dataGridView1, rbActivos.Checked
                            ? usuarioBLL.ListarUsuariosActivos()
                            : usuarioBLL.ListarTodosUsuarios());
                        MostrarCantidadUsuarios();
                        MessageBox.Show("Usuario Fue Eliminado", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case UserAction.Modify:
                        txtDni.ReadOnly = true;
                        if (ValidarCamposVacios(txtNom, txtApe, txtDni, txtUsuario, txtEmail) && ValidarComboBox(cbRol) && ValidarEntradaUsuario())
                        {
                            string dniOriginal = (dataGridView1.SelectedRows[0].DataBoundItem as Usuario).DNI;
                            string rolNuevo = cbRol.SelectedItem.ToString();

                            usuarioBLL.Modificar(dniOriginal, new Usuario(
                                txtDni.Text, txtNom.Text, txtApe.Text, txtUsuario.Text,
                                string.Empty, txtEmail.Text, default, default, rolNuevo));

                            // Si el usuario modificado es el que está en sesión, actualizarle los permisos
                            Usuario usuarioEnSesion = SessionManager.Instance.UsuarioActual();
                            if (usuarioEnSesion.DNI == dniOriginal)
                            {
                                var perfiles = perfilBLL.ObtenerPerfiles();
                                var perfilNuevo = perfiles.FirstOrDefault(p => p.Nombre == rolNuevo);
                                usuarioEnSesion.Permisos.Clear();
                                if (perfilNuevo != null)
                                    usuarioEnSesion.Permisos.Add(perfilNuevo);
                            }

                            txtDni.ReadOnly = false;
                            ReiniciarBotones();
                            MessageBox.Show("Usuario Fue Modificado", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MostrarUsuarios(dataGridView1, usuarioBLL.ListarUsuariosActivos());

                            // Si el usuario en sesión fue modificado y ya no tiene permiso para estar acá, salir
                            if (usuarioEnSesion.DNI == dniOriginal && !usuarioEnSesion.TienePermiso("Ver Usuarios"))
                            {
                                MessageBox.Show("Tu perfil fue modificado y ya no tenés acceso a esta sección.",
                                    "Acceso revocado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                btnCerrar_Click(sender, e);
                                return;
                            }

                            // Revalidar botones por si cambió el rol del usuario en sesión
                            ValidarPermisos();
                        }
                        break;

                    case UserAction.UnBlock:
                        ReiniciarBotones();
                        usuarioBLL.DesbloquearUsuario(dataGridView1.SelectedRows[0].DataBoundItem as Usuario);
                        MostrarUsuarios(dataGridView1, rbActivos.Checked
                            ? usuarioBLL.ListarUsuariosActivos()
                            : usuarioBLL.ListarTodosUsuarios());
                        MessageBox.Show("Usuario Fue Desbloqueado Y Clave Restaurada", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case UserAction.Activate:
                        usuarioBLL.EliminarLogico((dataGridView1.SelectedRows[0].DataBoundItem as Usuario).DNI);
                        ReiniciarBotones();
                        MostrarUsuarios(dataGridView1, rbActivos.Checked
                            ? usuarioBLL.ListarUsuariosActivos()
                            : usuarioBLL.ListarTodosUsuarios());
                        MessageBox.Show("Usuario Fue Activado", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case UserAction.Consult:
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
                    mensaje.AppendLine($"El Campo {nombreCampo} Esta Vacio");
                    if (primerInvalido == null) primerInvalido = txt;
                    hayVacios = true;
                }
            }

            if (hayVacios)
            {
                MessageBox.Show(mensaje.ToString(), "Faltan Completar Campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                primerInvalido?.Focus();
            }

            return !hayVacios;
        }

        private bool ValidarComboBox(ComboBox cb)
        {
            if (cb.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un perfil.", "Faltan Completar Campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cb.Focus();
                return false;
            }
            return true;
        }

        private bool ValidarEntradaUsuario()
        {
            StringBuilder errores = new StringBuilder();

            if (!Regex.IsMatch(txtNom.Text.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$"))
                errores.AppendLine("El Campo Nombre es inválido");

            if (!Regex.IsMatch(txtApe.Text.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$"))
                errores.AppendLine("El Campo Apellido es inválido");

            if (!Regex.IsMatch(txtDni.Text.Trim(), @"^\d{8}$"))
                errores.AppendLine("El Campo DNI debe tener 8 dígitos");

            if (userAction != UserAction.Add && (string.IsNullOrWhiteSpace(txtUsuario.Text) || txtUsuario.Text.Length < 4))
                errores.AppendLine("El Nombre de Usuario debe tener al menos 4 caracteres");

            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errores.AppendLine("El Correo Electrónico es inválido");

            if (errores.Length > 0)
            {
                MessageBox.Show(errores.ToString(), "Error Entrada Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
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

        private void btnCancelar_Click(object sender, EventArgs e) => ReiniciarBotones();

        private void ReiniciarBotones()
        {
            txtNom.Clear();
            txtApe.Clear();
            txtDni.Clear();
            txtEmail.Clear();
            txtUsuario.Clear();
            cbRol.SelectedIndex = -1;

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

            textBox1.Text = "Modo consulta";
            userAction = UserAction.Consult;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) =>
            MostrarUsuarios(dataGridView1, usuarioBLL.ListarTodosUsuarios());

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
                    cbRol.SelectedItem = user.Rol;
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].DataBoundItem is Usuario usuario)
                e.CellStyle.ForeColor = usuario.Bloqueado ? Color.Red : Color.Black;
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            userAction = UserAction.Add;
            textBox1.Text = "Modo Añadir";
            CargarPerfiles();
            EnabledControls(btnCrear, btnDesbloquear, btnModificar, btnActDes, btnAplicar, btnCancelar, btnSalir, pnFiltrado, panModificarUsuario, panel3);
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                userAction = UserAction.UnBlock;
                textBox1.Text = "Modo desbloquear";
                EnabledControls(btnCrear, btnDesbloquear, btnModificar, btnActDes, btnAplicar, btnCancelar, btnSalir, pnFiltrado);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            userAction = UserAction.Modify;
            CargarPerfiles();
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
                    textBox1.Text = "Modo activar";
                }
                else
                {
                    userAction = UserAction.Delete;
                    textBox1.Text = "Modo eliminar";
                }
                EnabledControls(btnCrear, btnDesbloquear, btnModificar, btnActDes, btnAplicar, btnCancelar, btnSalir, pnFiltrado, panModificarUsuario);
            }
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            LanguageManager.Instance.AgregarObservador(this);
            Actualizar(LanguageManager.Instance);
            ValidarPermisos();
        }

        private void ValidarPermisos()
        {
            Usuario usuarioActual = SessionManager.Instance.UsuarioActual();

            btnCrear.Enabled = usuarioActual.TienePermiso("Crear Usuario");
            btnModificar.Enabled = usuarioActual.TienePermiso("Modificar Usuario");
            btnActDes.Enabled = usuarioActual.TienePermiso("Activar Desactivar Usuario");
            btnDesbloquear.Enabled = usuarioActual.TienePermiso("Desbloquear Usuario");
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
            lblCantidadUsers.Text = lenguaje.GetTraduction("lblCantidadUsers");
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