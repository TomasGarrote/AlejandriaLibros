using Servicios;
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
    }
}