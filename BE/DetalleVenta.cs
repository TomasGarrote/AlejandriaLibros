using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class DetalleVenta
    {
        public int NroVenta { get; set; }
        public int CodigoLibro { get; set; }
        public string TituloLibro { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
        public string DVH { get; set; }
    }
}
