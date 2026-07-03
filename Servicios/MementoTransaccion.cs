using System;
using System.Collections.Generic;
using System.Text;

namespace Servicios.Patron_Memento
{
    public enum TipoOperacion { Asignacion, Eliminacion }
    public enum TipoComponente { Perfil, Familia, Permiso }

    public class MementoTransaccion
    {
        public string NombrePadre { get; private set; }
        public string NombreHijo { get; private set; }
        public TipoOperacion OperacionRealizada { get; private set; }
        public TipoComponente TipoDelComponente { get; private set; }

        public MementoTransaccion(string nomPadre, string nomHijo, TipoOperacion operacion, TipoComponente tipo)
        {
            NombrePadre = nomPadre;
            NombreHijo = nomHijo;
            OperacionRealizada = operacion;
            TipoDelComponente = tipo;
        }
    }
}