using System;
using System.Collections.Generic;
using System.Text;

namespace Servicios
{
    public enum EstadoAsignacion
    {
        Ok,
        ConflictoPermisos  
    }

    public class ResultadoAsignacion
    {
        public EstadoAsignacion Estado { get; set; }

        public List<string> PermisosConflictivos { get; set; } = new List<string>();

        public Dictionary<string, string> OrigenConflicto { get; set; }
            = new Dictionary<string, string>();
    }
}
