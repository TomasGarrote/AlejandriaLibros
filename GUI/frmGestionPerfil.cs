using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmGestionPerfiles : Form
    {
        int posX, posY;
        bool arrastrando = false;
        private readonly PerfilBLL _bll;
        private readonly string _login;
        private readonly Menu _menuPadre;

        private List<PermisoSimple> _permisos = new List<PermisoSimple>();
        private List<Familia> _familias = new List<Familia>();
        private List<Familia> _perfiles = new List<Familia>();

        private Familia _familiaSeleccionada = null;
        private Familia _perfilSeleccionado = null;

        public frmGestionPerfiles(string login)
        {
            InitializeComponent();
            _bll = new PerfilBLL();
            _login = login;

            ValidarPermisos();

            RbPermisos.CheckedChanged += RbPermisos_CheckedChanged;
            RbFamilias.CheckedChanged += RbFamilias_CheckedChanged_1;
            RbPerfiles.CheckedChanged += RbPerfiles_CheckedChanged;

            // !!! CORRECCIÓN AQUÍ: Asegurate de usar los que empiezan con 'pnl'
            // Si las líneas de abajo ya existen con 'pnl' en tu código, verificalas.
            // Si existen con 'panel', cambialas por 'pnl'.
            if (!this.Controls.Contains(pnlPermisos)) this.Controls.Add(pnlPermisos);
            if (!this.Controls.Contains(pnlFamilias)) this.Controls.Add(pnlFamilias);
            if (!this.Controls.Contains(pnlPerfiles)) this.Controls.Add(pnlPerfiles);
            

            this.SizeChanged += FrmGestionPerfiles_SizeChanged;

            // Forzar el estado inicial correcto
            RbPermisos.Checked = true;
            SincronizarVisibilidadPaneles();
            CargarDatosFormulario();
        }

        private void ValidarPermisos()
        {
          
            Usuario usuarioActual = SessionManager.Instance.UsuarioActual();
            if (!usuarioActual.TienePermiso(LanguageManager.Instance.GetTraduction("VerPerfiles")))
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("NoTienePermisosParaAcceder"), LanguageManager.Instance.GetTraduction("AccesoDenegado"), MessageBoxButtons.OK, MessageBoxIcon.Stop);

                if (Application.OpenForms["Menu"] != null)
                {
                    Application.OpenForms["Menu"].Show();
                }
                else
                {
                    Menu menu = new Menu();
                    menu.Show();
                }

                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

           
            ActualizarEstadoBotonesSegunPermisos();
        }

        private void ActualizarEstadoBotonesSegunPermisos()
        {
            Usuario usuarioActual = SessionManager.Instance.UsuarioActual();

            // Sección PERMISOS
            btnCrearPermiso.Enabled = usuarioActual.TienePermiso("Crear Permiso");
            btnEliminarPermiso.Enabled = usuarioActual.TienePermiso("Eliminar Permiso");

            // Sección FAMILIAS
            btnCrearFamilia.Enabled = usuarioActual.TienePermiso("Crear Familia");
            btnEliminarFamilia.Enabled = usuarioActual.TienePermiso("Eliminar Familia");
            btnAsignarPermisos.Enabled = usuarioActual.TienePermiso("Asignar Permiso Familia");
            btnQuitarPermisos.Enabled = usuarioActual.TienePermiso("Quitar Permiso Familia");
            btnAsignarSubfamilia.Enabled = usuarioActual.TienePermiso("Asignar Subfamilia");
            btnQuitarSubfamilia.Enabled = usuarioActual.TienePermiso("Quitar Subfamilia");

            // Sección PERFILES
            btnCrearPerfil.Enabled = usuarioActual.TienePermiso("Crear Perfil");
            btnEliminarPerfil.Enabled = usuarioActual.TienePermiso("Eliminar Perfil");
            btnAsignarFamiliaPerfil.Enabled = usuarioActual.TienePermiso("Asignar Familia Perfil");
            btnQuitarFamiliaPerfil.Enabled = usuarioActual.TienePermiso("Quitar Familia Perfil");
            BtnAsignarPermisoPerfil.Enabled = usuarioActual.TienePermiso("Asignar Permiso Perfil");
            BtnQuitarPermisoPerfil.Enabled = usuarioActual.TienePermiso("Quitar Permiso Perfil");
        }
        private void CentrarPanelesContenedores()
        {
            int contenedorAncho = this.ClientSize.Width;

            // 1. Identificar cuál panel 'pnl' está activo
            Panel panelActivo = null;
            if (RbPermisos.Checked) panelActivo = pnlPermisos;
            else if (RbFamilias.Checked) panelActivo = pnlFamilias;
            else if (RbPerfiles.Checked) panelActivo = pnlPerfiles;

            if (panelActivo != null)
            {
                // 2. Centrado horizontal idéntico (se mantiene perfecto)
                int xDelCentro = (contenedorAncho) / 2;

                // 3. NUEVA ALINEACIÓN VERTICAL: 
                // Tomamos el borde inferior de panel4 y le sumamos un margen de separación (ej. 20 píxeles)
                int margenSeparacion = 20;
                int yAlineada = panel4.Top - 10;

                // 4. Aplicamos la posición final (centrado horizontal, alineado vertical a panel4)
                panelActivo.Location = new Point(xDelCentro, yAlineada);
            }
        }

        private void FrmGestionPerfiles_SizeChanged(object sender, EventArgs e)
        {
            CentrarPanelesContenedores();
        }

        private void ActualizarTreeViewConsulta(Familia componenteRaiz)
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

            if (componenteRaiz != null)
            {
                _bll.ArmarArbolEstructural(componenteRaiz, treeView1.Nodes);
                treeView1.ExpandAll();
            }

            treeView1.EndUpdate();
        }


        private void CargarDatosFormulario()
        {
            try
            {
                string nombreFamAnterior = _familiaSeleccionada?.Nombre;
                string nombrePerfAnterior = _perfilSeleccionado?.Nombre;

                _permisos = _bll.ObtenerPermisos();
                _familias = _bll.ObtenerFamilias();
                _perfiles = _bll.ObtenerPerfiles();

                _familiaSeleccionada = !string.IsNullOrEmpty(nombreFamAnterior)
                    ? _familias.FirstOrDefault(f => f.Nombre == nombreFamAnterior)
                    : _familias.FirstOrDefault();

                _perfilSeleccionado = !string.IsNullOrEmpty(nombrePerfAnterior)
                    ? _perfiles.FirstOrDefault(p => p.Nombre == nombrePerfAnterior)
                    : _perfiles.FirstOrDefault();

                SincronizarVisibilidadPaneles();

                // NUEVO
                RefrescarSeguridad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    LanguageManager.Instance.GetTraduction("ErrorSincrop") + ex.Message,
                    LanguageManager.Instance.GetTraduction("ErrorOperacionalP"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SincronizarVisibilidadPaneles()
        {
            // 1. Sincronizar la visibilidad de los paneles 'pnl' correctos
            pnlPermisos.Visible = RbPermisos.Checked;
            pnlFamilias.Visible = RbFamilias.Checked;
            pnlPerfiles.Visible = RbPerfiles.Checked;

            // 2. Forzar a que el panel que quedó visible se acomode en el centro
            CentrarPanelesContenedores();

            // 3. Cargar los componentes correspondientes en el TreeView
            if (RbPermisos.Checked)
            {
                treeView1.Visible = false;
                treeView1.Nodes.Clear();
                CargarComponentesPermisos();
            }
            else if (RbFamilias.Checked)
            {
                treeView1.Visible = true;
                CargarComponentesFamilias();
            }
            else if (RbPerfiles.Checked)
            {
                treeView1.Visible = true;
                CargarComponentesPerfiles();
            }
        }

        private void CargarComponentesPermisos()
        {
            dgvPermisos.DataSource = null;
            dgvPermisos.DataSource = _permisos.Select(p => new
            {
                Nombre = p.Nombre,
                Tipo = "Permiso Simple"
            }).ToList();
        }

        private void BtnCrearPermiso_Click(object sender, EventArgs e)
        {
            string nombre = txtNombrePermiso.Text.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("IngreseUnNombreParaPermisoP"), LanguageManager.Instance.GetTraduction("CampoRequeridoP"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombrePermiso.Focus();
                return;
            }

            try
            {
                _bll.CrearPermiso(nombre, _login);
                txtNombrePermiso.Clear();
                CargarDatosFormulario();
                MessageBox.Show($"{LanguageManager.Instance.GetTraduction("Permisomsjp")} '{nombre}' {LanguageManager.Instance.GetTraduction("CreadoExitoP")}", LanguageManager.Instance.GetTraduction("ExitoP"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, LanguageManager.Instance.GetTraduction("ErrorP"), MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnEliminarPermiso_Click(object sender, EventArgs e)
        {
            if (dgvPermisos.CurrentRow == null) return;
            string nombre = dgvPermisos.CurrentRow.Cells["Nombre"].Value.ToString();

            if (MessageBox.Show($"¿{LanguageManager.Instance.GetTraduction("EliminarP")} '{nombre}'?", LanguageManager.Instance.GetTraduction("Confirmarp"), MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                _bll.EliminarPermiso(nombre, _login);
                CargarDatosFormulario();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void CargarComponentesFamilias()
        {
            lstFamilias.SelectedIndexChanged -= LstFamilias_SelectedIndexChanged;
            lstFamilias.DataSource = null;
            lstFamilias.DataSource = _familias;
            lstFamilias.DisplayMember = "Nombre";

            if (_familiaSeleccionada != null)
            {
                lstFamilias.SelectedItem = _familias.FirstOrDefault(f => f.Nombre == _familiaSeleccionada.Nombre);
            }

            lstFamilias.SelectedIndexChanged += LstFamilias_SelectedIndexChanged;
            VisualizarDetallesFamilia();
        }

        private void VisualizarDetallesFamilia()
        {
            if (_familiaSeleccionada == null)
            {
                clbPermisosDisp.DataSource = null;
                clbPermisosAsig.DataSource = null;
                clbSubfamiliasDisp.DataSource = null;
                clbSubfamiliasAsig.DataSource = null;
                treeView1.Nodes.Clear();
                return;
            }

            clbPermisosDisp.DataSource = _bll.ObtenerPermisosDisponibles(_familiaSeleccionada);
            clbPermisosDisp.DisplayMember = "Nombre";

            clbPermisosAsig.DataSource = _familiaSeleccionada.ListaHijos.OfType<PermisoSimple>().ToList();
            clbPermisosAsig.DisplayMember = "Nombre";

            clbSubfamiliasDisp.DataSource = _bll.ObtenerFamiliasDisponibles(_familiaSeleccionada.Nombre)
                .Where(f => !_familiaSeleccionada.ListaHijos.Any(x => x.Nombre == f.Nombre)).ToList();
            clbSubfamiliasDisp.DisplayMember = "Nombre";

            clbSubfamiliasAsig.DataSource = _familiaSeleccionada.ListaHijos.OfType<Familia>().ToList();
            clbSubfamiliasAsig.DisplayMember = "Nombre";

            ActualizarTreeViewConsulta(_familiaSeleccionada);
        }

        private void LstFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            _familiaSeleccionada = lstFamilias.SelectedItem as Familia;
            VisualizarDetallesFamilia();
        }

        private void BtnCrearFamilia_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreFamilia.Text.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("ingreseNombreFamiliaP"), LanguageManager.Instance.GetTraduction("CampoRequeridoP"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreFamilia.Focus();
                return;
            }

            try
            {
                _bll.CrearFamilia(nombre, _login);
                txtNombreFamilia.Clear();
                CargarDatosFormulario();
                MessageBox.Show($"{LanguageManager.Instance.GetTraduction("Familiap")} '{nombre}' {LanguageManager.Instance.GetTraduction("CreadoExitoP")}", LanguageManager.Instance.GetTraduction("ExitoP"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, LanguageManager.Instance.GetTraduction("ErrorP"), MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnAsignarPermisos_Click(object sender, EventArgs e)
        {
            if (_familiaSeleccionada == null) return;
            var seleccionados = clbPermisosDisp.CheckedItems.Cast<PermisoSimple>().Select(p => p.Nombre).ToList();
            if (!seleccionados.Any()) return;

            foreach (var permiso in seleccionados)
            {
                var res = _bll.AsignarComponentesHijos(_familiaSeleccionada.Nombre, new List<string> { permiso }, true, _login);

                if (res.Estado == EstadoAsignacion.ConflictoPermisos)
                {
                    string detalleError = res.OrigenConflicto[permiso];

                    if (detalleError != null && detalleError.Contains("Redundancia detectada"))
                    {
                        MostrarModalResolucionHeredada(_familiaSeleccionada.Nombre, permiso, permiso, (estrategia) =>
                        {
                            if (estrategia == "RESOLVER")
                            {
                                _bll.EliminarPermisoRedundanteDeNodoContenedor(_familiaSeleccionada.Nombre, new List<string> { permiso }, _login);
                                var reintento = _bll.AsignarComponentesHijos(_familiaSeleccionada.Nombre, new List<string> { permiso }, true, _login);
                                if (reintento.Estado == EstadoAsignacion.Ok)
                                {
                                    MessageBox.Show($"{LanguageManager.Instance.GetTraduction("Permisomsjp")} '{permiso}' {LanguageManager.Instance.GetTraduction("UcExitoEnLaRaizp")} '{_familiaSeleccionada.Nombre}' {LanguageManager.Instance.GetTraduction("RemovidoDeSubfami")}", LanguageManager.Instance.GetTraduction("ExitoP"));
                                }
                            }
                            CargarDatosFormulario();
                        });
                        return;
                    }

                    if (detalleError != null && detalleError.StartsWith("CONFLICTO_HORIZONTAL_DETALLADO|"))
                    {
                        var partes = detalleError.Split('|');
                        string perm = partes[1];
                        string nodoDest = partes[2];
                        string ancestro = partes[3];
                        string ramaColat = partes[4];
                        string contenedorDirecto = partes[5];

                        MostrarModalResolucionHorizontal(ancestro, ramaColat, contenedorDirecto, nodoDest, perm, (estrategia) =>
                        {
                            if (estrategia == "RESOLVER")
                            {
                                _bll.EliminarPermisoDeContenedorEspecifico(contenedorDirecto, perm, _login);
                                var reintento = _bll.AsignarComponentesHijos(nodoDest, new List<string> { perm }, true, _login);
                                if (reintento.Estado == EstadoAsignacion.Ok)
                                {
                                    MessageBox.Show($"{LanguageManager.Instance.GetTraduction("Permisomsjp")} '{perm}' {LanguageManager.Instance.GetTraduction("AsConExitoaP")} '{nodoDest}' {LanguageManager.Instance.GetTraduction("TrasResolverRedun")}", LanguageManager.Instance.GetTraduction("TrasResolverRedun"));
                                }
                            }
                            CargarDatosFormulario();
                        });
                        return;
                    }

                    MessageBox.Show(detalleError, LanguageManager.Instance.GetTraduction("validacionesDePermisosmsj"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            CargarDatosFormulario();
            MessageBox.Show(LanguageManager.Instance.GetTraduction("PermisosAsignadosCorrectmsjP"), LanguageManager.Instance.GetTraduction("ExitoP"));
        }

        private void BtnQuitarPermisos_Click(object sender, EventArgs e)
        {
            if (_familiaSeleccionada == null) return;
            var seleccionados = clbPermisosAsig.CheckedItems.Cast<PermisoSimple>().Select(p => p.Nombre).ToList();
            if (!seleccionados.Any()) return;

            _bll.QuitarHijos(_familiaSeleccionada.Nombre, seleccionados, true, _login);
            CargarDatosFormulario();
            MessageBox.Show(LanguageManager.Instance.GetTraduction("PermisosRemovep"), LanguageManager.Instance.GetTraduction("ExitoP"));
        }

        private void BtnAsignarSubfamilia_Click(object sender, EventArgs e)
        {
            if (_familiaSeleccionada == null) return;
            var seleccionadas = clbSubfamiliasDisp.CheckedItems.Cast<Familia>().Select(f => f.Nombre).ToList();
            if (!seleccionadas.Any()) return;

            foreach (var subfamiliaHijo in seleccionadas)
            {
                var res = _bll.AsignarComponentesHijos(_familiaSeleccionada.Nombre, new List<string> { subfamiliaHijo }, false, _login);

                if (res.Estado == EstadoAsignacion.ConflictoPermisos)
                {
                    string claveConflicto = res.OrigenConflicto.Values.FirstOrDefault();

                    if (claveConflicto != null && claveConflicto.StartsWith("CONFLICTO_REDUNDANCIA_HEREDADA|"))
                    {
                        var partes = claveConflicto.Split('|');
                        var permisosConflictivos = partes[1].Split(',').ToList();
                        string subFamiliaConflictiva = partes[2];
                        string nodoDondeYaExiste = partes[3];

                        MostrarModalResolucionHeredada(nodoDondeYaExiste, subFamiliaConflictiva, partes[1], (estrategia) =>
                        {
                            if (estrategia == "RESOLVER")
                            {
                                _bll.EliminarPermisoRedundanteDeNodoContenedor(_familiaSeleccionada.Nombre, permisosConflictivos, _login);
                                var reintento = _bll.AsignarComponentesHijos(_familiaSeleccionada.Nombre, new List<string> { subfamiliaHijo }, false, _login);
                                if (reintento.Estado == EstadoAsignacion.Ok)
                                {
                                    MessageBox.Show($"'{subfamiliaHijo}' {LanguageManager.Instance.GetTraduction("AsigCorrecmsjP")}", LanguageManager.Instance.GetTraduction("ExitoP"));
                                }
                            }
                            CargarDatosFormulario();
                        });
                        return;
                    }

                    MessageBox.Show(claveConflicto, LanguageManager.Instance.GetTraduction("Restriccionp"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            CargarDatosFormulario();
            MessageBox.Show(LanguageManager.Instance.GetTraduction("SubFamiliaAsigCorrectp"), LanguageManager.Instance.GetTraduction("ExitoP"));
        }

        private void BtnQuitarSubfamilia_Click(object sender, EventArgs e)
        {
            if (_familiaSeleccionada == null) return;
            var seleccionadas = clbSubfamiliasAsig.CheckedItems.Cast<Familia>().Select(f => f.Nombre).ToList();
            if (!seleccionadas.Any()) return;

            _bll.QuitarHijos(_familiaSeleccionada.Nombre, seleccionadas, false, _login);
            CargarDatosFormulario();
            MessageBox.Show(LanguageManager.Instance.GetTraduction("SubFamiliaRemovep"), LanguageManager.Instance.GetTraduction("ExitoP"));
        }

        private void BtnEliminarFamilia_Click(object sender, EventArgs e)
        {
            if (_familiaSeleccionada == null) return;
            if (MessageBox.Show($"¿{LanguageManager.Instance.GetTraduction("EstasSeguroEliFami")} '{_familiaSeleccionada.Nombre}'? {LanguageManager.Instance.GetTraduction("EstasAcciQuiToRelap")}", LanguageManager.Instance.GetTraduction("ConfirmarElimip"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                _bll.EliminarFamiliaOPerfil(_familiaSeleccionada.Nombre, _login);

                string nombreEliminado = _familiaSeleccionada.Nombre;
                _familiaSeleccionada = null;

                CargarDatosFormulario();
                MessageBox.Show($"La familia '{nombreEliminado}' se eliminó correctamente del sistema.", LanguageManager.Instance.GetTraduction("ExitoP"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LanguageManager.Instance.GetTraduction("RestrIntegraJerarmsjP"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarComponentesPerfiles()
        {
            lstPerfiles.SelectedIndexChanged -= LstPerfiles_SelectedIndexChanged;
            lstPerfiles.DataSource = null;
            lstPerfiles.DataSource = _perfiles;
            lstPerfiles.DisplayMember = "Nombre";

            if (_perfilSeleccionado != null)
            {
                lstPerfiles.SelectedItem = _perfiles.FirstOrDefault(p => p.Nombre == _perfilSeleccionado.Nombre);
            }

            lstPerfiles.SelectedIndexChanged += LstPerfiles_SelectedIndexChanged;
            VisualizarDetallesPerfil();
        }

        private void VisualizarDetallesPerfil()
        {
            if (_perfilSeleccionado == null)
            {
                clbFamiliasDispPerfil.DataSource = null;
                clbFamiliasAsigPerfil.DataSource = null;
                clbPermisosDispPerfil.DataSource = null;
                clbPermisosAsigPerfil.DataSource = null;
                treeView1.Nodes.Clear();
                return;
            }

            clbFamiliasDispPerfil.DataSource = _bll.ObtenerFamiliasDisponibles(_perfilSeleccionado.Nombre)
                .Where(f => !_perfilSeleccionado.ListaHijos.Any(x => x.Nombre == f.Nombre)).ToList();
            clbFamiliasDispPerfil.DisplayMember = "Nombre";

            clbFamiliasAsigPerfil.DataSource = _perfilSeleccionado.ListaHijos.OfType<Familia>().ToList();
            clbFamiliasAsigPerfil.DisplayMember = "Nombre";

            clbPermisosDispPerfil.DataSource = _bll.ObtenerPermisosDisponibles(_perfilSeleccionado);
            clbPermisosDispPerfil.DisplayMember = "Nombre";

            clbPermisosAsigPerfil.DataSource = _perfilSeleccionado.ListaHijos.OfType<PermisoSimple>().ToList();
            clbPermisosAsigPerfil.DisplayMember = "Nombre";

            ActualizarTreeViewConsulta(_perfilSeleccionado);
        }

        private void LstPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            _perfilSeleccionado = lstPerfiles.SelectedItem as Familia;
            VisualizarDetallesPerfil();
        }

        private void BtnCrearPerfil_Click(object sender, EventArgs e)
        {
            string nombre = txtNombrePerfil.Text.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show(LanguageManager.Instance.GetTraduction("ingUnNombrePerfilP"), LanguageManager.Instance.GetTraduction("CampoRequeridoP"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombrePerfil.Focus();
                return;
            }

            try
            {
                _bll.CrearPerfil(nombre, _login);
                txtNombrePerfil.Clear();
                CargarDatosFormulario();
                MessageBox.Show($"{LanguageManager.Instance.GetTraduction("PerfilmsjP")} '{nombre}'{LanguageManager.Instance.GetTraduction("CreadoExitoP")}", LanguageManager.Instance.GetTraduction("ExitoP"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message,LanguageManager.Instance.GetTraduction("ErrorP") , MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnAsignarFamiliaPerfil_Click(object sender, EventArgs e)
        {
            if (_perfilSeleccionado == null) return;
            var seleccionados = clbFamiliasDispPerfil.CheckedItems.Cast<Familia>().Select(f => f.Nombre).ToList();
            if (!seleccionados.Any()) return;

            foreach (var familia in seleccionados)
            {
                var res = _bll.AsignarComponentesHijos(_perfilSeleccionado.Nombre, new List<string> { familia }, false, _login);

                if (res.Estado == EstadoAsignacion.ConflictoPermisos)
                {
                    string detalleError = res.OrigenConflicto[familia];

                    if (detalleError != null && detalleError.StartsWith("CONFLICTO_REDUNDANCIA_HEREDADA|"))
                    {
                        var partes = detalleError.Split('|');
                        var permisosConflictivos = partes[1].Split(',').ToList();
                        string subFamiliaConflictiva = partes[2];
                        string nodoDondeYaExiste = partes[3];

                        MostrarModalResolucionHeredada(nodoDondeYaExiste, subFamiliaConflictiva, partes[1], (estrategia) =>
                        {
                            if (estrategia == "RESOLVER")
                            {
                                _bll.EliminarPermisoRedundanteDeNodoContenedor(_perfilSeleccionado.Nombre, permisosConflictivos, _login);
                                var reintento = _bll.AsignarComponentesHijos(_perfilSeleccionado.Nombre, new List<string> { subFamiliaConflictiva }, false, _login);
                                if (reintento.Estado == EstadoAsignacion.Ok)
                                {
                                    MessageBox.Show($"{LanguageManager.Instance.GetTraduction("Familiap")} '{subFamiliaConflictiva}' {LanguageManager.Instance.GetTraduction("integradaAlPerfilExitosamentep")}", LanguageManager.Instance.GetTraduction("ExitoP"));
                                }
                            }
                            CargarDatosFormulario();
                        });
                        return;
                    }

                    MessageBox.Show(detalleError, LanguageManager.Instance.GetTraduction("ValiEstrucPerfP"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            CargarDatosFormulario();
            MessageBox.Show(LanguageManager.Instance.GetTraduction("FamiAsigAPerfCorrecp"), LanguageManager.Instance.GetTraduction("ExitoP"));
        }

        private void BtnQuitarFamiliaPerfil_Click(object sender, EventArgs e)
        {
            if (_perfilSeleccionado == null) return;
            var seleccionadas = clbFamiliasAsigPerfil.CheckedItems.Cast<Familia>().Select(f => f.Nombre).ToList();
            if (!seleccionadas.Any()) return;

            _bll.QuitarHijos(_perfilSeleccionado.Nombre, seleccionadas, false, _login);
            CargarDatosFormulario();
            MessageBox.Show(LanguageManager.Instance.GetTraduction("FamiRemovep"), LanguageManager.Instance.GetTraduction("ExitoP"));
        }

        private void BtnAsignarPermisoPerfil_Click(object sender, EventArgs e)
        {
            if (_perfilSeleccionado == null) return;
            var seleccionados = clbPermisosDispPerfil.CheckedItems.Cast<PermisoSimple>().Select(p => p.Nombre).ToList();
            if (!seleccionados.Any()) return;

            foreach (var permiso in seleccionados)
            {
                var res = _bll.AsignarComponentesHijos(_perfilSeleccionado.Nombre, new List<string> { permiso }, true, _login);

                if (res.Estado == EstadoAsignacion.ConflictoPermisos)
                {
                    string detalleError = res.OrigenConflicto[permiso];

                    if (detalleError != null && detalleError.Contains("Redundancia detectada"))
                    {
                        MostrarModalResolucionHeredada(_perfilSeleccionado.Nombre, permiso, permiso, (estrategia) =>
                        {
                            if (estrategia == "RESOLVER")
                            {
                                _bll.EliminarPermisoRedundanteDeNodoContenedor(_perfilSeleccionado.Nombre, new List<string> { permiso }, _login);
                                var reintento = _bll.AsignarComponentesHijos(_perfilSeleccionado.Nombre, new List<string> { permiso }, true, _login);
                                if (reintento.Estado == EstadoAsignacion.Ok)
                                {
                                    MessageBox.Show($"{LanguageManager.Instance.GetTraduction("Permisomsjp")} '{permiso}' {LanguageManager.Instance.GetTraduction("UnifiConExitoRaizFamilia")} '{_perfilSeleccionado.Nombre}' {LanguageManager.Instance.GetTraduction("YRemoveDeSusFamilp")}", LanguageManager.Instance.GetTraduction("ExitoP"));
                                }
                            }
                            CargarDatosFormulario();
                        });
                        return;
                    }

                    if (detalleError != null && detalleError.StartsWith("CONFLICTO_HORIZONTAL_DETALLADO|"))
                    {
                        var partes = detalleError.Split('|');
                        string perm = partes[1];
                        string nodoDest = partes[2];
                        string ancestro = partes[3];
                        string ramaColat = partes[4];
                        string contenedorDirecto = partes[5];

                        MostrarModalResolucionHorizontal(ancestro, ramaColat, contenedorDirecto, nodoDest, perm, (estrategia) =>
                        {
                            if (estrategia == "RESOLVER")
                            {
                                _bll.EliminarPermisoDeContenedorEspecifico(contenedorDirecto, perm, _login);
                                var reintento = _bll.AsignarComponentesHijos(nodoDest, new List<string> { perm }, true, _login);
                                if (reintento.Estado == EstadoAsignacion.Ok)
                                {
                                    MessageBox.Show($"{LanguageManager.Instance.GetTraduction("Permisomsjp")} '{perm}' {LanguageManager.Instance.GetTraduction("UnificandoExtoPermisop")} '{nodoDest}'.", LanguageManager.Instance.GetTraduction("ExitoP"));
                                }
                            }
                            CargarDatosFormulario();
                        });
                        return;
                    }

                    MessageBox.Show(detalleError, LanguageManager.Instance.GetTraduction("ValiPerPerfp"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            CargarDatosFormulario();
            MessageBox.Show(LanguageManager.Instance.GetTraduction("PermiAsigPerfCorrectP"), LanguageManager.Instance.GetTraduction("ExitoP"));
        }

        private void BtnQuitarPermisoPerfil_Click(object sender, EventArgs e)
        {
            if (_perfilSeleccionado == null) return;
            var seleccionados = clbPermisosAsigPerfil.CheckedItems.Cast<PermisoSimple>().Select(p => p.Nombre).ToList();
            if (!seleccionados.Any()) return;

            _bll.QuitarHijos(_perfilSeleccionado.Nombre, seleccionados, true, _login);
            CargarDatosFormulario();
            MessageBox.Show(LanguageManager.Instance.GetTraduction("PermisosRemovep"), LanguageManager.Instance.GetTraduction("ExitoP"));
        }

        private void BtnEliminarPerfil_Click(object sender, EventArgs e)
        {
            if (_perfilSeleccionado == null) return;
            if (MessageBox.Show($"¿{LanguageManager.Instance.GetTraduction("SeguEliminaPerfP")} '{_perfilSeleccionado.Nombre}'?", LanguageManager.Instance.GetTraduction("ConfirmarElimip"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                _bll.EliminarFamiliaOPerfil(_perfilSeleccionado.Nombre, _login);

                string nombreEliminado = _perfilSeleccionado.Nombre;
                _perfilSeleccionado = null;

                CargarDatosFormulario();
                MessageBox.Show($"{LanguageManager.Instance.GetTraduction("ELPerfilP")} '{nombreEliminado}' {LanguageManager.Instance.GetTraduction("SeEliminodelSistema")}", LanguageManager.Instance.GetTraduction("ExitoP"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,LanguageManager.Instance.GetTraduction("RestAsigUsup") , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MostrarModalResolucionHorizontal(string ancestro, string ramaColateral, string contenedorDirecto, string nodoDestino, string permiso, Action<string> callback)
        {
            Control panelActivo = RbPerfiles.Checked ? (Control)panelPerfiles : (Control)panelFamilias;
            panelActivo.Enabled = false;

            Panel pnlModal = new Panel
            {
                Name = "pnlModalHorizontal",
                Size = new Size(600, 390),
                BackColor = Color.FromArgb(255, 244, 244),
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlModal.Location = new Point((this.ClientSize.Width - pnlModal.Width) / 2, (this.ClientSize.Height - pnlModal.Height) / 2);

            var lblTitulo = new Label
            {
                Text = LanguageManager.Instance.GetTraduction("AlerCritiP"),
                Location = new Point(15, 15),
                Size = new Size(570, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.Firebrick
            };

            string textoExplicativo = "";

            if (RbFamilias.Checked)
            {
                string familiaActual = _familiaSeleccionada != null ? _familiaSeleccionada.Nombre : nodoDestino;

                textoExplicativo =
                    $"{LanguageManager.Instance.GetTraduction("textexpli1")} '{permiso}' {LanguageManager.Instance.GetTraduction("textexpli2")} '{familiaActual}'.\n\n" +
                    $"[{LanguageManager.Instance.GetTraduction("text3")}]:\n" +
                    $"{LanguageManager.Instance.GetTraduction("text4")} '{ancestro}').\n\n" +
                    $"{LanguageManager.Instance.GetTraduction("Text5")} '{ramaColateral}', {LanguageManager.Instance.GetTraduction("text6")} '{contenedorDirecto}').\n\n" +
                    $"{LanguageManager.Instance.GetTraduction("text7")} '{permiso}' {LanguageManager.Instance.GetTraduction("text8")}";
            }
            else
            {
                string perfilActual = _perfilSeleccionado != null ? _perfilSeleccionado.Nombre : nodoDestino;

                if (ancestro == perfilActual)
                {
                    textoExplicativo =
                        $"Acción: Intentás asignar el permiso '{permiso}' en la raíz del perfil '{perfilActual}'.\n\n" +
                        $"[RECHAZADO POR REDUNDANCIA DIRECTA]:\n" +
                        $"El perfil '{perfilActual}' YA posee y hereda este permiso de manera limpia a través de su familia interna '{contenedorDirecto}'. No se requiere duplicarlo en la raíz.";
                }
                else
                {
                    textoExplicativo =
                        $"Acción: Intentás asignar el permiso '{permiso}' al perfil '{perfilActual}'.\n\n" +
                        $"[RECHAZADO POR CONFLICTO HORIZONTAL]:\n" +
                        $"Existe una colisión estructural en el árbol. El rol superior '{ancestro}' ya contiene dicho acceso en la rama '{ramaColateral}'.";
                }
            }

            var lblDescripcion = new Label
            {
                Text = textoExplicativo,
                Location = new Point(15, 45),
                Size = new Size(570, 170),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(50, 50, 50)
            };

            var rbtnResolver = new RadioButton
            {
                Text = $"Resolver automáticamente (Quitar permiso redundante de '{contenedorDirecto}' y mantenerlo en '{nodoDestino}')",
                Location = new Point(20, 230),
                Size = new Size(560, 30),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Checked = true
            };

            var rbtnCancelar = new RadioButton
            {
                Text = "Cancelar la operación (Mantener estructura limpia sin modificaciones)",
                Location = new Point(20, 265),
                Size = new Size(560, 30),
                Font = new Font("Segoe UI", 9F)
            };

            var btnEjecutar = new Button
            {
                Text = "Procesar Cambio",
                Location = new Point(220, 320),
                Size = new Size(160, 35),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.Firebrick,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnEjecutar.Click += (s, e) =>
            {
                string estrategia = rbtnResolver.Checked ? "RESOLVER" : "CANCELAR";
                this.Controls.Remove(pnlModal);
                pnlModal.Dispose();
                panelActivo.Enabled = true;
                callback?.Invoke(estrategia);
            };

            pnlModal.Controls.Add(lblTitulo);
            pnlModal.Controls.Add(lblDescripcion);
            pnlModal.Controls.Add(rbtnResolver);
            pnlModal.Controls.Add(rbtnCancelar);
            pnlModal.Controls.Add(btnEjecutar);

            this.Controls.Add(pnlModal);
            pnlModal.BringToFront();
        }

        private void MostrarModalResolucionHeredada(string nodoDondeYaExiste, string hijo, string permisos, Action<string> callback)
        {
            Control panelActivo = RbPerfiles.Checked ? (Control)panelPerfiles : (Control)panelFamilias;
            panelActivo.Enabled = false;

            Panel pnlModal = new Panel
            {
                Name = "pnlModalConflictos",
                Size = new Size(500, 280),
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlModal.Location = new Point((this.ClientSize.Width - pnlModal.Width) / 2, (this.ClientSize.Height - pnlModal.Height) / 2);

            var lblTitulo = new Label
            {
                Text = "RESOLUCIÓN DE CONFLICTOS JERÁRQUICOS",
                Location = new Point(15, 15),
                Size = new Size(470, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40)
            };

            var lblDescripcion = new Label
            {
                Text = $"El componente '{hijo}' que intentás vincular incluye o colisiona con los permisos: [{permisos}].\n" +
                       $"Estos accesos ya se encuentran presentes en la raíz de '{nodoDondeYaExiste}'.\n\n" +
                       $"¿Qué deseas hacer?",
                Location = new Point(15, 45),
                Size = new Size(470, 75),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            var rbtnResolver = new RadioButton
            {
                Text = $"Optimizar estructura (Quitar redundancias de los subnodos y unificar)",
                Location = new Point(20, 135),
                Size = new Size(460, 25),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Checked = true
            };

            var rbtnCancelar = new RadioButton
            {
                Text = "Cancelar la operación (No realizar modificaciones)",
                Location = new Point(20, 165),
                Size = new Size(460, 25),
                Font = new Font("Segoe UI", 9F)
            };

            var btnEjecutar = new Button
            {
                Text = "Confirmar",
                Location = new Point(170, 215),
                Size = new Size(160, 32),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(45, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnEjecutar.Click += (s, e) =>
            {
                string estrategia = rbtnResolver.Checked ? "RESOLVER" : "CANCELAR";
                this.Controls.Remove(pnlModal);
                pnlModal.Dispose();
                panelActivo.Enabled = true;
                callback?.Invoke(estrategia);
            };

            pnlModal.Controls.Add(lblTitulo);
            pnlModal.Controls.Add(lblDescripcion);
            pnlModal.Controls.Add(rbtnResolver);
            pnlModal.Controls.Add(rbtnCancelar);
            pnlModal.Controls.Add(btnEjecutar);

            this.Controls.Add(pnlModal);
            pnlModal.BringToFront();
        }

        private void RbPermisos_CheckedChanged(object sender, EventArgs e)
        {
            if (RbPermisos.Checked)
            {
            
                ActualizarEstadoBotonesSegunPermisos();
                SincronizarVisibilidadPaneles();
            }
        }

        private void RbFamilias_CheckedChanged_1(object sender, EventArgs e)
        {
            if (RbFamilias.Checked)
            {
                ActualizarEstadoBotonesSegunPermisos();
                SincronizarVisibilidadPaneles();
            }
        }

        private void RbPerfiles_CheckedChanged(object sender, EventArgs e)
        {
            if (RbPerfiles.Checked)
            {
                ActualizarEstadoBotonesSegunPermisos();
                SincronizarVisibilidadPaneles();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Menu"] != null)
            {
                Application.OpenForms["Menu"].Show();
            }
            else
            {
                Menu menu = new Menu();
                menu.Show();
            }
            this.Close();
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



        private void RefrescarSeguridad()
        {
            ActualizarEstadoBotonesSegunPermisos();

            Usuario usuarioActual = SessionManager.Instance.UsuarioActual();

            if (!usuarioActual.TienePermiso("Ver Perfiles"))
            {
                MessageBox.Show(
                    "Sus permisos fueron modificados. Ya no posee acceso a esta pantalla.",
                    "Permisos actualizados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                if (Application.OpenForms["Menu"] != null)
                {
                    Application.OpenForms["Menu"].Show();
                }
                else
                {
                    Menu menu = new Menu();
                    menu.Show();
                }

                this.Close();
            }
        }

        private void frmGestionPerfiles_Load(object sender, EventArgs e)
        {

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