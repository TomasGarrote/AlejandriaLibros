using System;
using System.Collections.Generic;

namespace Servicios
{
    public abstract class ComponentePermiso
    {
        public string Nombre { get; set; }

        public abstract void AgregarHijo(ComponentePermiso hijo);
        public abstract void QuitarHijo(ComponentePermiso hijo);
        public abstract List<ComponentePermiso> ObtenerHijos();

       
        public abstract bool TienePermiso(string nombrePermiso);
    }
}