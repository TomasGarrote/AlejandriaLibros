using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Libro
    {
        public int CodigoInterno { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }
        public string Ubicacion { get; set; }
        public decimal Precio { get; set; }
        public int StockDisponible { get; set; }
        public int Activo { get; set; }
        public string DVH { get; set; }
    }
}
