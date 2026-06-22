using System;
using System.Collections.Generic;

namespace Servicios
{
    public class Familia : ComponentePermiso
    {
        public bool EsRol { get; set; }
        private List<ComponentePermiso> _hijos = new List<ComponentePermiso>();

        public List<ComponentePermiso> ListaHijos
        {
            get { return _hijos; }
            set { _hijos = value; }
        }

        public override void AgregarHijo(ComponentePermiso hijo)
        {
            if (hijo != null && !_hijos.Contains(hijo))
            {
                _hijos.Add(hijo);
            }
        }

        public override void QuitarHijo(ComponentePermiso hijo)
        {
            if (hijo != null && _hijos.Contains(hijo))
            {
                _hijos.Remove(hijo);
            }
        }

        public override List<ComponentePermiso> ObtenerHijos()
        {
            return _hijos;
        }

 
        public override bool TienePermiso(string nombrePermiso)
        {
          
            if (this.Nombre != null && this.Nombre.Equals(nombrePermiso, StringComparison.OrdinalIgnoreCase))
                return true;

            // 2. Si no, recorro a todos mis hijos usando recursividad
            foreach (var hijo in _hijos)
            {
                if (hijo.TienePermiso(nombrePermiso))
                    return true; // Encontrado en las profundidades del árbol
            }

            return false; // No se encontró en esta rama
        }
    }
}