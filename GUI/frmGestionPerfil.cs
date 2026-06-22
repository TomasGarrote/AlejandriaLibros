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
        private readonly PerfilBLL _bll;
        private readonly string _login;

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

         
            RbPermisos.CheckedChanged += RbPermisos_CheckedChanged;
            RbFamilias.CheckedChanged += RbFamilias_CheckedChanged_1;
            RbPerfiles.CheckedChanged += RbPerfiles_CheckedChanged;

            this.Controls.Add(panelPermisos);
            this.Controls.Add(panelFamilias);
            this.Controls.Add(panelPerfiles);

            this.SizeChanged += FrmGestionPerfiles_SizeChanged;
            CentrarPanelesContenedores();

            CargarDatosFormulario();
        }

        private void CentrarPanelesContenedores()
        {
            int contenedorAncho = this.ClientSize.Width;
            int contenedorAlto = this.ClientSize.Height;

            int panelAncho = panelFamilias.Width;
            int panelAlto = panelFamilias.Height;

            int xDelCentro = (contenedorAncho - panelAncho) / 2;
            int yDelCentro = (contenedorAlto - panelAlto) / 2;

            if (yDelCentro < 60) yDelCentro = 60;

            Point posicionCentral = new Point(xDelCentro, yDelCentro);

            panelPermisos.Location = posicionCentral;
            panelFamilias.Location = posicionCentral;
            panelPerfiles.Location = posicionCentral;
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

        private void FrmGestionPerfiles_SizeChanged(object sender, EventArgs e)
        {
            CentrarPanelesContenedores();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al sincronizar datos del repositorio: " + ex.Message, "Error Operacional", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SincronizarVisibilidadPaneles()
        {
            panelPermisos.Visible = RbPermisos.Checked;
            panelFamilias.Visible = RbFamilias.Checked;
            panelPerfiles.Visible = RbPerfiles.Checked;

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
                MessageBox.Show("Por favor, ingrese un nombre para el nuevo permiso.", "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombrePermiso.Focus();
                return;
            }

            try
            {
                _bll.CrearPermiso(nombre, _login);
                txtNombrePermiso.Clear();
                CargarDatosFormulario();
                MessageBox.Show($"Permiso '{nombre}' creado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnEliminarPermiso_Click(object sender, EventArgs e)
        {
            if (dgvPermisos.CurrentRow == null) return;
            string nombre = dgvPermisos.CurrentRow.Cells["Nombre"].Value.ToString();

            if (MessageBox.Show($"¿Eliminar '{nombre}'?", "Confirmar", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

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
                MessageBox.Show("Por favor, ingrese un nombre para la nueva familia.", "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreFamilia.Focus();
                return;
            }

            try
            {
                _bll.CrearFamilia(nombre, _login);
                txtNombreFamilia.Clear();
                CargarDatosFormulario();
                MessageBox.Show($"Familia '{nombre}' creada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
                  
                        MostrarPanelResolucionConflictos(_familiaSeleccionada.Nombre, permiso, _familiaSeleccionada.Nombre, (estrategia) =>
                        {
                            if (estrategia == "RESOLVER")
                            {
                           
                                _bll.EliminarPermisoRedundanteDeNodoContenedor(_familiaSeleccionada.Nombre, new List<string> { permiso }, _login);

                        
                                var reintento = _bll.AsignarComponentesHijos(_familiaSeleccionada.Nombre, new List<string> { permiso }, true, _login);
                                if (reintento.Estado == EstadoAsignacion.Ok)
                                {
                                    MessageBox.Show($"Permiso '{permiso}' unificado con éxito en la raíz '{_familiaSeleccionada.Nombre}' y removido de las subfamilias.", "Éxito");
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

                        MostrarPanelResolucionHorizontal(ancestro, ramaColat, contenedorDirecto, nodoDest, perm, (estrategia) =>
                        {
                            if (estrategia == "RESOLVER")
                            {
                                _bll.EliminarPermisoDeContenedorEspecifico(contenedorDirecto, perm, _login);
                                var reintento = _bll.AsignarComponentesHijos(nodoDest, new List<string> { perm }, true, _login);
                                if (reintento.Estado == EstadoAsignacion.Ok)
                                {
                                    MessageBox.Show($"Permiso '{perm}' asignado con éxito a '{nodoDest}' tras resolver la redundancia.", "Éxito");
                                }
                            }
                            CargarDatosFormulario();
                        });
                        return;
                    }

                  
                    MessageBox.Show(detalleError, "Validación de Permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            CargarDatosFormulario();
            MessageBox.Show("Permisos asignados correctamente.", "Éxito");
        }

        private void MostrarPanelResolucionHorizontal(string ancestro, string ramaColateral, string contenedorDirecto, string nodoDestino, string permiso, Action<string> callback)
        {
            panelFamilias.Enabled = false;

            Panel pnlModal = new Panel
            {
                Name = "pnlModalHorizontal",
                Size = new Size(560, 340),
                BackColor = Color.FromArgb(253, 244, 244),
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlModal.Location = new Point((this.ClientSize.Width - pnlModal.Width) / 2, (this.ClientSize.Height - pnlModal.Height) / 2);

            var lblTitulo = new Label
            {
                Text = "ALERTA: CONFLICTO DE REDUNDANCIA HORIZONTAL",
                Location = new Point(15, 15),
                Size = new Size(530, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.Firebrick
            };

            string textoExplicativo =
                $"Estás intentando asignar el permiso '{permiso}' a la familia '{nodoDestino}'.\n\n" +
                $"¿Por qué no es posible de forma directa?:\n" +
                $"Ambas ramas confluyen en el ancestro común '{ancestro}'. El permiso que buscás agregar YA está siendo heredado a través de la rama paralela '{ramaColateral}' (específicamente dentro de '{contenedorDirecto}').\n\n" +
                $"Asignarlo aquí generaría accesos redundantes en la estructura.";

            var lblDescripcion = new Label
            {
                Text = textoExplicativo,
                Location = new Point(15, 45),
                Size = new Size(530, 120),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(40, 40, 40)
            };

            var rbtnResolver = new RadioButton
            {
                Text = $"Resolver automáticamente (Quitar permiso de '{contenedorDirecto}' y asignarlo a '{nodoDestino}')",
                Location = new Point(20, 180),
                Size = new Size(520, 30),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Checked = true
            };

            var rbtnCancelar = new RadioButton
            {
                Text = "Cancelar la operación (Mantener estructura actual)",
                Location = new Point(20, 215),
                Size = new Size(520, 30),
                Font = new Font("Segoe UI", 9F)
            };

            var btnEjecutar = new Button
            {
                Text = "Procesar Cambio",
                Location = new Point(200, 270),
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
                panelFamilias.Enabled = true;
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
        private void BtnQuitarPermisos_Click(object sender, EventArgs e)
        {
            if (_familiaSeleccionada == null) return;
            var seleccionados = clbPermisosAsig.CheckedItems.Cast<PermisoSimple>().Select(p => p.Nombre).ToList();
            if (!seleccionados.Any()) return;

            _bll.QuitarHijos(_familiaSeleccionada.Nombre, seleccionados, true, _login);
            CargarDatosFormulario();
            MessageBox.Show("Permisos removidos.", "Éxito");
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
                        string nodoDondeYaExiste = partes[3];

                        MostrarPanelResolucionConflictos(nodoDondeYaExiste, subfamiliaHijo, partes[1], (estrategia) =>
                        {
                            if (estrategia == "RESOLVER")
                            {
                                _bll.EliminarPermisoRedundanteDeNodoContenedor(_familiaSeleccionada.Nombre, permisosConflictivos, _login);
                                var reintento = _bll.AsignarComponentesHijos(_familiaSeleccionada.Nombre, new List<string> { subfamiliaHijo }, false, _login);
                                if (reintento.Estado == EstadoAsignacion.Ok)
                                {
                                    MessageBox.Show($"'{subfamiliaHijo}' asignado correctamente.", "Éxito");
                                }
                            }
                            CargarDatosFormulario();
                        });
                        return;
                    }

                    MessageBox.Show(claveConflicto, "Restricción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            CargarDatosFormulario();
            MessageBox.Show("Subfamilias asignadas correctamente.", "Éxito");
        }

        private void BtnQuitarSubfamilia_Click(object sender, EventArgs e)
        {
            if (_familiaSeleccionada == null) return;
            var seleccionadas = clbSubfamiliasAsig.CheckedItems.Cast<Familia>().Select(f => f.Nombre).ToList();
            if (!seleccionadas.Any()) return;

            _bll.QuitarHijos(_familiaSeleccionada.Nombre, seleccionadas, false, _login);
            CargarDatosFormulario();
            MessageBox.Show("Subfamilias removidas.", "Éxito");
        }

     
        private void BtnEliminarFamilia_Click(object sender, EventArgs e)
        {
            if (_familiaSeleccionada == null) return;
            if (MessageBox.Show($"¿Está seguro de que desea eliminar la familia '{_familiaSeleccionada.Nombre}'? Esta acción quitará todas sus relaciones.", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
            
                _bll.EliminarFamiliaOPerfil(_familiaSeleccionada.Nombre, _login);

                string nombreEliminado = _familiaSeleccionada.Nombre;
                _familiaSeleccionada = null;

                CargarDatosFormulario();
                MessageBox.Show($"La familia '{nombreEliminado}' se eliminó correctamente del sistema.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
         
                MessageBox.Show(ex.Message, "Restricción de Integridad Jerárquica", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Por favor, ingrese un nombre para el nuevo perfil.", "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombrePerfil.Focus();
                return;
            }

            try
            {
                _bll.CrearPerfil(nombre, _login);
                txtNombrePerfil.Clear();
                CargarDatosFormulario();
                MessageBox.Show($"Perfil '{nombre}' creado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
                        string listaPermisos = partes[1];
                        string subFamiliaConflictiva = partes[2];
                        string nodoResponsable = partes[3];

                        MostrarPanelResolucionConflictos(subFamiliaConflictiva, listaPermisos, nodoResponsable, (estrategia) =>
                        {
                            if (estrategia == "RESOLVER")
                            {
                                var permisosAQuitar = listaPermisos.Split(',').ToList();
                                foreach (var perm in permisosAQuitar)
                                {
                                    _bll.EliminarPermisoRedundanteDeNodoContenedor(nodoResponsable, new List<string> { perm }, _login);
                                }

                                var reintento = _bll.AsignarComponentesHijos(_perfilSeleccionado.Nombre, new List<string> { subFamiliaConflictiva }, false, _login);
                                if (reintento.Estado == EstadoAsignacion.Ok)
                                {
                                    MessageBox.Show($"Familia '{subFamiliaConflictiva}' integrada al perfil con éxito.", "Éxito");
                                }
                            }
                            CargarDatosFormulario();
                        });
                        return; 
                    }

                    MessageBox.Show(detalleError, "Validación de Estructura en Perfil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            CargarDatosFormulario();
            MessageBox.Show("Familias asignadas al perfil correctamente.", "Éxito");
        }

        private void BtnQuitarFamiliaPerfil_Click(object sender, EventArgs e)
        {
            if (_perfilSeleccionado == null) return;
            var seleccionadas = clbFamiliasAsigPerfil.CheckedItems.Cast<Familia>().Select(f => f.Nombre).ToList();
            if (!seleccionadas.Any()) return;

            _bll.QuitarHijos(_perfilSeleccionado.Nombre, seleccionadas, false, _login);
            CargarDatosFormulario();
            MessageBox.Show("Familias removidas.", "Éxito");
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
                     
                        MostrarPanelResolucionConflictos(_perfilSeleccionado.Nombre, permiso, _perfilSeleccionado.Nombre, (estrategia) =>
                        {
                            if (estrategia == "RESOLVER")
                            {
                             
                                _bll.EliminarPermisoRedundanteDeNodoContenedor(_perfilSeleccionado.Nombre, new List<string> { permiso }, _login);

                              
                                var reintento = _bll.AsignarComponentesHijos(_perfilSeleccionado.Nombre, new List<string> { permiso }, true, _login);
                                if (reintento.Estado == EstadoAsignacion.Ok)
                                {
                                    MessageBox.Show($"Permiso '{permiso}' unificado con éxito en la raíz del perfil '{_perfilSeleccionado.Nombre}' y removido de sus subfamilias.", "Éxito");
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

                        MostrarPanelResolucionHorizontal(ancestro, ramaColat, contenedorDirecto, nodoDest, perm, (estrategia) =>
                        {
                            if (estrategia == "RESOLVER")
                            {
                                _bll.EliminarPermisoDeContenedorEspecifico(contenedorDirecto, perm, _login);
                                var reintento = _bll.AsignarComponentesHijos(nodoDest, new List<string> { perm }, true, _login);
                                if (reintento.Estado == EstadoAsignacion.Ok)
                                {
                                    MessageBox.Show($"Permiso '{perm}' unificado con éxito en el perfil '{nodoDest}'.", "Éxito");
                                }
                            }
                            CargarDatosFormulario();
                        });
                        return;
                    }

                   
                    MessageBox.Show(detalleError, "Validación de Permisos en Perfil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            CargarDatosFormulario();
            MessageBox.Show("Permisos asignados al perfil correctamente.", "Éxito");
        }

        private void BtnQuitarPermisoPerfil_Click(object sender, EventArgs e)
        {
            if (_perfilSeleccionado == null) return;
            var seleccionados = clbPermisosAsigPerfil.CheckedItems.Cast<PermisoSimple>().Select(p => p.Nombre).ToList();
            if (!seleccionados.Any()) return;

            _bll.QuitarHijos(_perfilSeleccionado.Nombre, seleccionados, true, _login);
            CargarDatosFormulario();
            MessageBox.Show("Permisos removidos.", "Éxito");
        }

        private void BtnEliminarPerfil_Click(object sender, EventArgs e)
        {
            if (_perfilSeleccionado == null) return;
            if (MessageBox.Show($"¿Está seguro de que desea eliminar el perfil '{_perfilSeleccionado.Nombre}'?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
               
                _bll.EliminarFamiliaOPerfil(_perfilSeleccionado.Nombre, _login);

                string nombreEliminado = _perfilSeleccionado.Nombre;
                _perfilSeleccionado = null;

                CargarDatosFormulario();
                MessageBox.Show($"El perfil '{nombreEliminado}' se eliminó correctamente del sistema.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
            
                MessageBox.Show(ex.Message, "Restricción de Asignación de Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

   

        private void MostrarPanelResolucionConflictos(string nodoDondeYaExiste, string hijo, string permisos, Action<string> callback)
        {
            panelFamilias.Enabled = false;

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
                Text = $"La subfamilia '{hijo}' que intenta añadir incluye los permisos [{permisos}].\n" +
                       $"Estos ya existen en '{nodoDondeYaExiste}'.\n\n" +
                       $"¿Qué deseas hacer?",
                Location = new Point(15, 45),
                Size = new Size(470, 75),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            var rbtnResolver = new RadioButton
            {
                Text = $"Eliminar los permisos de '{nodoDondeYaExiste}' y asignar '{hijo}'",
                Location = new Point(20, 135),
                Size = new Size(460, 25),
                Font = new Font("Segoe UI", 9F),
                Checked = true
            };

            var rbtnCancelar = new RadioButton
            {
                Text = "Cancelar la operación (no hacer cambios)",
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
                panelFamilias.Enabled = true;
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
            if (RbPermisos.Checked) SincronizarVisibilidadPaneles();
        }

    
        private void RbFamilias_CheckedChanged_1(object sender, EventArgs e)
        {
            if (RbFamilias.Checked) SincronizarVisibilidadPaneles();
        }

        private void RbPerfiles_CheckedChanged(object sender, EventArgs e)
        {
            if (RbPerfiles.Checked) SincronizarVisibilidadPaneles();
        }
    }
}