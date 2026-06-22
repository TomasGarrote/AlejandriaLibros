using Servicios;
using System;
using System.Collections.Generic;

namespace Servicios
{
    public class PermisoSimple : ComponentePermiso
    {
        public override void AgregarHijo(ComponentePermiso hijo)
        {
            // Un permiso simple es una hoja, no puede tener hijos.
            throw new NotImplementedException("No se pueden agregar componentes a un permiso simple.");
        }

        public override void QuitarHijo(ComponentePermiso hijo)
        {
            throw new NotImplementedException("No se pueden quitar componentes de un permiso simple.");
        }

        public override List<ComponentePermiso> ObtenerHijos()
        {
            return new List<ComponentePermiso>(); // Devuelve lista vacía siempre
        }
        public override bool TienePermiso(string nombrePermiso)
        {
            return this.Nombre != null && this.Nombre.Equals(nombrePermiso, StringComparison.OrdinalIgnoreCase);
        }
    }
}