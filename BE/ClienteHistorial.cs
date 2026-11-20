using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class ClienteHistorial
    {
        public int IdHistorial { get; set; }
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaCambio { get; set; }
        public string Operacion { get; set; }
        public string Usuario { get; set; }
        public bool VersionActiva { get; set; }
        public string DVH { get; set; }
    }
}
