using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Cliente
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int Activo { get; set; } = 1;
        public string DVH { get; set; }
    }
}
