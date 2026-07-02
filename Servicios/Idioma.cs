using System;
using System.Collections.Generic;
using System.Text;

namespace Servicios
{
    public class Idioma
    {
        public string DNI { get; set; }
        public string UserName { get; set; }
        public string CodigoIdioma { get; set; }
        public string DVH { get; set; }

        public Idioma()
        {
            
        }
        public Idioma(string userName, string codIdioma, string dVH)
        {
            UserName = userName;
            CodigoIdioma = codIdioma;
            DVH = dVH;
        }
    }
}
