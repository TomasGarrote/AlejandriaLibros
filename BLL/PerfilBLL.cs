using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BLL
{
    public class PerfilBLL
    {
        private readonly PerfilDAL _dal;
        private readonly BitacoraBLL _bitacoraBLL;
        private const string MODULO_BITACORA = "Perfiles";

        public PerfilBLL()
        {
            _dal = new PerfilDAL();
            _bitacoraBLL = new BitacoraBLL();
        }

        public List<PermisoSimple> ObtenerPermisos() => _dal.ObtenerPermisos();

        public List<Familia> ObtenerFamilias()
        {
            return _dal.ObtenerFamiliasYPerfiles().Where(f => !f.EsRol).ToList();
        }

        public List<Familia> ObtenerPerfiles()
        {
            return _dal.ObtenerFamiliasYPerfiles().Where(f => f.EsRol).ToList();
        }

        public List<PermisoSimple> ObtenerPermisosDisponibles(Familia componentePadre)
        {
            var todosLosPermisos = _dal.ObtenerPermisos();
            if (componentePadre == null) return todosLosPermisos;

            var permisosDirectos = new HashSet<string>();
            foreach (var hijo in componentePadre.ListaHijos.OfType<PermisoSimple>())
            {
                permisosDirectos.Add(hijo.Nombre);
            }

            return todosLosPermisos.Where(p => !permisosDirectos.Contains(p.Nombre)).ToList();
        }

        public List<Familia> ObtenerFamiliasDisponibles(string nombrePadre)
        {
            var todasLasFamilias = ObtenerFamilias();
            var todosLosComponentes = _dal.ObtenerFamiliasYPerfiles();
            var padre = todosLosComponentes.FirstOrDefault(f => f.Nombre == nombrePadre);

            if (padre == null) return todasLasFamilias;

            var permisosAsignados = new HashSet<string>();
            var familiasAsignadas = new HashSet<string>();
            ObtenerEstructuraPlana(padre, permisosAsignados, familiasAsignadas);

            return todasLasFamilias.Where(f => f.Nombre != nombrePadre && !familiasAsignadas.Contains(f.Nombre)).ToList();
        }


        public void CrearPermiso(string nombre, string usuario)
        {
           
            var nuevoPermiso = new PermisoSimple { Nombre = nombre };
            _dal.GuardarPermiso(nuevoPermiso);
            RegistrarEnBitacora(usuario, $"Crear permiso simple: {nombre}", 1);
        }

        public void EliminarPermiso(string nombre, string usuario)
        {
            _dal.EliminarPermiso(nombre);
            RegistrarEnBitacora(usuario, $"Eliminar permiso simple: {nombre}", 1);
        }

        public void CrearFamilia(string nombre, string usuario)
        {
          
            var nuevaFamilia = new Familia { Nombre = nombre, EsRol = false };
            _dal.GuardarFamilia(nuevaFamilia);
            RegistrarEnBitacora(usuario, $"Crear familia: {nombre}", 1);
        }

        public void CrearPerfil(string nombre, string usuario)
        {
          
            var nuevoPerfil = new Familia { Nombre = nombre, EsRol = true };
            _dal.GuardarFamilia(nuevoPerfil);
            RegistrarEnBitacora(usuario, $"Crear perfil (Rol): {nombre}", 1);
        }

        public void EliminarFamiliaOPerfil(string nombre, string usuario)
        {
            try
            {
            
                var todosLosComponentes = _dal.ObtenerFamiliasYPerfiles();
                var target = todosLosComponentes.FirstOrDefault(f => f.Nombre == nombre);

                if (target == null)
                {
                    throw new Exception($"El componente '{nombre}' no existe en el sistema.");
                }

          
                if (target.EsRol)
                {
                   
                    if (_dal.ElPerfilEstaAsignadoAUsuarios(target.Nombre))
                    {
                        throw new Exception($"No se puede eliminar el perfil '{target.Nombre}' porque está asignado a usuarios activos en el sistema. Desvincule a los usuarios primero.");
                    }
                }
                else
                {
                   
                    if (_dal.LaFamiliaEstaEnUsoComoHijo(target.Nombre))
                    {
                        throw new Exception($"No se puede eliminar la familia '{target.Nombre}' porque está siendo utilizada como subcomponente de otra familia o perfil.");
                    }
                }

              
                _dal.EliminarFamilia(target);
                RegistrarEnBitacora(usuario, $"Eliminar contenedor jerárquico: {nombre}", 1);
            }
            catch (Exception ex)
            {
               
                RegistrarEnBitacora(usuario, $"ERROR al intentar eliminar '{nombre}': {ex.Message}", 3);
                throw new Exception(ex.Message);
            }
        }

        public void QuitarHijos(string nombrePadre, List<string> hijos, bool esPermisoSimple, string usuario)
        {
            try
            {
                var todosLosComponentes = _dal.ObtenerFamiliasYPerfiles();
                var padre = todosLosComponentes.FirstOrDefault(f => f.Nombre == nombrePadre);

                if (padre == null)
                {
                    throw new Exception($"No se encontró el contenedor padre '{nombrePadre}'.");
                }

               
                padre.ListaHijos.RemoveAll(h => hijos.Contains(h.Nombre) &&
                    ((esPermisoSimple && h is PermisoSimple) || (!esPermisoSimple && h is Familia)));

                
                _dal.GuardarRelaciones(padre);

                foreach (var hijo in hijos)
                {
                    string tipoNodo = esPermisoSimple ? "Permiso Simple" : "Subfamilia";
                    RegistrarEnBitacora(usuario, $"Desvincular {tipoNodo} '{hijo}' del contenedor '{nombrePadre}'", 1);
                }
            }
            catch (Exception ex)
            {
                RegistrarEnBitacora(usuario, $"ERROR en QuitarHijos de '{nombrePadre}': {ex.Message}", 3);
                throw new Exception("Error operativo al desvincular los componentes: " + ex.Message);
            }
        }

        public void EliminarPermisoRedundanteDeNodoContenedor(string nombreContenedorRaiz, List<string> permisosComponentes, string usuario)
        {
            try
            {
                var todosLosComponentes = _dal.ObtenerFamiliasYPerfiles();
                var raiz = todosLosComponentes.FirstOrDefault(f => f.Nombre == nombreContenedorRaiz);
                if (raiz == null) throw new Exception($"No se encontró el nodo raíz '{nombreContenedorRaiz}'.");

                foreach (var permiso in permisosComponentes)
                {
                    string contenedorDirectoNombre = BuscarContenedorDirectoDelPermiso(raiz, permiso);
                    if (!string.IsNullOrEmpty(contenedorDirectoNombre))
                    {
                        var contenedorDirectoObj = todosLosComponentes.FirstOrDefault(f => f.Nombre == contenedorDirectoNombre);
                        if (contenedorDirectoObj != null)
                        {
                            contenedorDirectoObj.ListaHijos.RemoveAll(h => h.Nombre == permiso && h is PermisoSimple);
                            _dal.GuardarRelaciones(contenedorDirectoObj);

                            RegistrarEnBitacora(usuario, $"Mitigación de redundancia: Remoción del permiso '{permiso}' en subnodo '{contenedorDirectoNombre}'", 2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RegistrarEnBitacora(usuario, $"ERROR en EliminarPermisoRedundante: {ex.Message}", 1);
                throw new Exception("Error operativo al purgar los componentes redundantes: " + ex.Message);
            }
        }

        public void EliminarPermisoDeContenedorEspecifico(string nombreContenedor, string permiso, string usuario)
        {
            try
            {
                var todosLosComponentes = _dal.ObtenerFamiliasYPerfiles();
                var contenedor = todosLosComponentes.FirstOrDefault(f => f.Nombre == nombreContenedor);

                if (contenedor != null)
                {
                    contenedor.ListaHijos.RemoveAll(h => h.Nombre == permiso && h is PermisoSimple);
                    _dal.GuardarRelaciones(contenedor);

                    RegistrarEnBitacora(usuario, $"Resolución Horizontal: Remoción automática de '{permiso}' en el subnodo '{nombreContenedor}' para unificar accesos.", 2);
                }
            }
            catch (Exception ex)
            {
                RegistrarEnBitacora(usuario, $"ERROR en EliminarPermisoDeContenedorEspecifico: {ex.Message}", 1);
                throw new Exception("Error operativo al purgar el permiso: " + ex.Message);
            }
        }

        public ResultadoAsignacion AsignarComponentesHijos(string nombrePadre, List<string> nombresHijos, bool esPermisoSimple, string identificadorUsuario)
        {
            var resultado = new ResultadoAsignacion { Estado = EstadoAsignacion.Ok };
            if (!nombresHijos.Any()) return resultado;

            var todosLosComponentes = _dal.ObtenerFamiliasYPerfiles();
            var componentePadre = todosLosComponentes.FirstOrDefault(f => f.Nombre == nombrePadre);

            if (componentePadre == null) return resultado;


            foreach (var nombreHijo in nombresHijos)
            {
                if (nombrePadre == nombreHijo)
                {
                    resultado.Estado = EstadoAsignacion.ConflictoPermisos;
                    resultado.PermisosConflictivos.Add(nombreHijo);
                    resultado.OrigenConflicto[nombreHijo] = "Restricción: No es posible asignar un componente jerárquico a sí mismo.";
                    return resultado;
                }

                if (!esPermisoSimple)
                {
                    var componenteHijo = todosLosComponentes.FirstOrDefault(f => f.Nombre == nombreHijo);
                    if (componenteHijo != null && componenteHijo.EsRol && !componentePadre.EsRol)
                    {
                        resultado.Estado = EstadoAsignacion.ConflictoPermisos;
                        resultado.PermisosConflictivos.Add(nombreHijo);
                        resultado.OrigenConflicto[nombreHijo] = $"Incompatibilidad de estructura: El perfil '{nombreHijo}' no puede ser subcomponente de la familia '{nombrePadre}'.";
                        return resultado;
                    }

                    if (ValidarCicloJerarquico(nombreHijo, nombrePadre, todosLosComponentes))
                    {
                        resultado.Estado = EstadoAsignacion.ConflictoPermisos;
                        resultado.PermisosConflictivos.Add(nombreHijo);
                        resultado.OrigenConflicto[nombreHijo] = $"Referencia circular detectada: El componente '{nombreHijo}' ya contiene al componente '{nombrePadre}'.";
                        return resultado;
                    }
                }

                var permisosActualesPadre = new HashSet<string>();
                var familiasActualesPadre = new HashSet<string>();
                ObtenerEstructuraPlana(componentePadre, permisosActualesPadre, familiasActualesPadre);

                foreach (var elemento in componentePadre.ListaHijos.OfType<PermisoSimple>())
                {
                    permisosActualesPadre.Add(elemento.Nombre);
                }

                if (esPermisoSimple)
                {
                    if (permisosActualesPadre.Contains(nombreHijo))
                    {
                        resultado.Estado = EstadoAsignacion.ConflictoPermisos;
                        resultado.PermisosConflictivos.Add(nombreHijo);
                        resultado.OrigenConflicto[nombreHijo] = $"Redundancia detectada: El permiso '{nombreHijo}' ya existe en '{nombrePadre}'.";
                        return resultado;
                    }

                    string conflictoHorizontal = VerificarConflictoHorizontal(todosLosComponentes, componentePadre, nombreHijo);
                    if (!string.IsNullOrEmpty(conflictoHorizontal))
                    {
                        resultado.Estado = EstadoAsignacion.ConflictoPermisos;
                        resultado.PermisosConflictivos.Add(nombreHijo);
                        resultado.OrigenConflicto[nombreHijo] = conflictoHorizontal;
                        return resultado;
                    }
                }
                else
                {
                    var subFamiliaHijo = todosLosComponentes.FirstOrDefault(f => f.Nombre == nombreHijo);
                    if (subFamiliaHijo != null)
                    {
                        var permisosDelHijo = new HashSet<string>();
                        var familiasDelHijo = new HashSet<string>();
                        ObtenerEstructuraPlana(subFamiliaHijo, permisosDelHijo, familiasDelHijo);

                        foreach (var elemento in subFamiliaHijo.ListaHijos.OfType<PermisoSimple>())
                        {
                            permisosDelHijo.Add(elemento.Nombre);
                        }

                        if (familiasActualesPadre.Contains(nombreHijo))
                        {
                            resultado.Estado = EstadoAsignacion.ConflictoPermisos;
                            resultado.PermisosConflictivos.Add(nombreHijo);
                            resultado.OrigenConflicto[nombreHijo] = $"Redundancia estructural: La subfamilia '{nombreHijo}' ya forma parte de '{nombrePadre}'.";
                            return resultado;
                        }

                        var colisionesPermisos = permisosActualesPadre.Intersect(permisosDelHijo).ToList();
                        if (colisionesPermisos.Any())
                        {
                            string permisoEjemplo = colisionesPermisos.First();
                            string nodoResponsable = BuscarContenedorDirectoDelPermiso(componentePadre, permisoEjemplo) ?? componentePadre.Nombre;

                            resultado.Estado = EstadoAsignacion.ConflictoPermisos;
                            resultado.PermisosConflictivos.Add(nombreHijo);
                            resultado.OrigenConflicto[nombreHijo] = $"CONFLICTO_REDUNDANCIA_HEREDADA|{string.Join(",", colisionesPermisos)}|{nombreHijo}|{nodoResponsable}";
                            return resultado;
                        }
                    }
                }
            }

            if (resultado.Estado == EstadoAsignacion.ConflictoPermisos) return resultado;

      
            foreach (var nombreHijo in nombresHijos)
            {
                if (esPermisoSimple)
                {
                    componentePadre.AgregarHijo(new PermisoSimple { Nombre = nombreHijo });
                }
                else
                {
                    var subFam = todosLosComponentes.FirstOrDefault(f => f.Nombre == nombreHijo);
                    if (subFam != null) componentePadre.AgregarHijo(subFam);
                }

                string tipoNodo = esPermisoSimple ? "Permiso Simple" : "Subfamilia";
                RegistrarEnBitacora(identificadorUsuario, $"Asignación exitosa de {tipoNodo} '{nombreHijo}' a la raíz '{nombrePadre}'", 1);
            }

          
            _dal.GuardarRelaciones(componentePadre);

            return resultado;
        }


        private string VerificarConflictoHorizontal(List<Familia> todosLosComponentes, Familia nodoDestino, string permisoBuscar)
        {
            var ancestrosRaiz = todosLosComponentes.Where(posiblePadre => ContieneHijoRecursivo(posiblePadre, nodoDestino.Nombre)).ToList();

            foreach (var ancestro in ancestrosRaiz)
            {
                foreach (var hijoFam in ancestro.ListaHijos.OfType<Familia>())
                {
                    if (hijoFam.Nombre == nodoDestino.Nombre) continue;

                    if (TienePermisoHeredado(hijoFam, permisoBuscar))
                    {
                        string contenedorDirecto = BuscarContenedorDirectoDelPermiso(hijoFam, permisoBuscar) ?? hijoFam.Nombre;
                        return $"CONFLICTO_HORIZONTAL_DETALLADO|{permisoBuscar}|{nodoDestino.Nombre}|{ancestro.Nombre}|{hijoFam.Nombre}|{contenedorDirecto}";
                    }
                }

                foreach (var hijoPermiso in ancestro.ListaHijos.OfType<PermisoSimple>())
                {
                    if (hijoPermiso.Nombre == permisoBuscar)
                    {
                        return $"CONFLICTO_HORIZONTAL_DETALLADO|{permisoBuscar}|{nodoDestino.Nombre}|{ancestro.Nombre}|{ancestro.Nombre}|{ancestro.Nombre}";
                    }
                }
            }
            return null;
        }

        private bool ContieneHijoRecursivo(Familia padre, string nombreHijoBuscar)
        {
            if (padre.ListaHijos.OfType<Familia>().Any(f => f.Nombre == nombreHijoBuscar || ContieneHijoRecursivo(f, nombreHijoBuscar)))
                return true;
            if (padre.ListaHijos.OfType<PermisoSimple>().Any(p => p.Nombre == nombreHijoBuscar))
                return true;
            return false;
        }

        private bool TienePermisoHeredado(Familia familia, string permisoBuscar)
        {
            if (familia.Nombre == permisoBuscar) return true;
            if (familia.ListaHijos.OfType<PermisoSimple>().Any(p => p.Nombre == permisoBuscar))
                return true;

            foreach (var subFam in familia.ListaHijos.OfType<Familia>())
            {
                if (TienePermisoHeredado(subFam, permisoBuscar)) return true;
            }
            return false;
        }

        private string BuscarContenedorDirectoDelPermiso(Familia nodoActual, string nombrePermiso)
        {
            if (nodoActual.ListaHijos.OfType<PermisoSimple>().Any(p => p.Nombre == nombrePermiso))
                return nodoActual.Nombre;

            foreach (var subFamilia in nodoActual.ListaHijos.OfType<Familia>())
            {
                string encontrado = BuscarContenedorDirectoDelPermiso(subFamilia, nombrePermiso);
                if (!string.IsNullOrEmpty(encontrado)) return encontrado;
            }

            return null;
        }

        private bool ValidarCicloJerarquico(string actual, string objetivo, List<Familia> componentes)
        {
            var comp = componentes.FirstOrDefault(f => f.Nombre == actual);
            if (comp == null) return false;
            if (comp.ListaHijos.Any(h => h.Nombre == objetivo)) return true;

            foreach (var sub in comp.ListaHijos.OfType<Familia>())
            {
                if (ValidarCicloJerarquico(sub.Nombre, objetivo, componentes)) return true;
            }
            return false;
        }

        private void ObtenerEstructuraPlana(Familia componente, HashSet<string> permisos, HashSet<string> familias)
        {
            foreach (var hijo in componente.ListaHijos)
            {
                if (hijo is PermisoSimple)
                {
                    permisos.Add(hijo.Nombre);
                }
                else if (hijo is Familia fam)
                {
                    familias.Add(fam.Nombre);
                    ObtenerEstructuraPlana(fam, permisos, familias);
                }
            }
        }

        private void RegistrarEnBitacora(string usuario, string evento, int criticidad)
        {
            try
            {
                var registro = new Bitacora
                {
                    Login = !string.IsNullOrEmpty(usuario) ? usuario : "Sistema",
                    Fecha = DateTime.Now,
                    Modulo = MODULO_BITACORA,
                    Evento = evento,
                    Criticidad = criticidad
                };
                _bitacoraBLL.RegistrarEvento(registro);
            }
            catch { }
        }

        public void ArmarArbolEstructural(Familia nodoActual, TreeNodeCollection nodosUI)
        {
            var nuevoNodo = new TreeNode(nodoActual.Nombre)
            {
                Tag = nodoActual,
                ImageIndex = nodoActual.EsRol ? 0 : 1
            };

            nodosUI.Add(nuevoNodo);

            foreach (var hijo in nodoActual.ListaHijos)
            {
                if (hijo is Familia subFamilia)
                {
                    ArmarArbolEstructural(subFamilia, nuevoNodo.Nodes);
                }
                else if (hijo is PermisoSimple permiso)
                {
                    var nodoHoja = new TreeNode(permiso.Nombre)
                    {
                        Tag = permiso,
                        ImageIndex = 2
                    };
                    nuevoNodo.Nodes.Add(nodoHoja);
                }
            }
        }
    }
}