using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Venta
    {
        public int NroVenta { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string DniUsuario { get; set; }
        public string MedioDePago { get; set; }
        public decimal Descuento { get; set; } // CU-003
        public decimal Subtotal { get; set; }
        public decimal Total => Subtotal - Descuento;
        public List<DetalleVenta> Detalle { get; set; } = new List<DetalleVenta>();
        public string DVH { get; set; }
    }
}
