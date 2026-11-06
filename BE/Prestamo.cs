using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Prestamo
    {
        public int NroPrestamo { get; set; }
        public string Estado { get; set; }
        public string CodigoInterno { get; set; }
        public DateTime FechaProgramada { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaDevolucionReal { get; set; }
        public int Dni_Cliente { get; set; }
        public int Dni_Usuario { get; set; }
        public string DVH { get; set; }
    }
}
