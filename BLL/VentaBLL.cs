using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BLL
{
    public class VentaBLL
    {
        private readonly LibroBLL libroBLL = new LibroBLL();
        private readonly VentaDAL ventaDAL = new VentaDAL();
        private readonly BitacoraBLL _bitacoraBLL = new BitacoraBLL();
        private readonly DigitoVerificadorBLL _digitoVerificadorBLL = new DigitoVerificadorBLL();
        private readonly SerializacionService _serializacionService = new SerializacionService();

        public List<Libro> ObtenerCatalogo()
        {
            try
            {
                var lista = libroBLL.ListarDisponibles();
                if (lista.Count == 0)
                    throw new Exception($"{LanguageManager.Instance.GetTraduction("msjNoHayEjemplares")}");
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public List<Libro> FiltrarCatalogo(string categoria, string autor, string titulo)
        {
            try
            {
                var lista = libroBLL.ListarLibrosPorFiltro(categoria, autor, titulo, "", "");
                    
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ValidarAgregarAlCarrito(BindingList<DetalleVenta> carrito, int codigoInterno, int cantidadSolicitada)
        {
            try
            {
                var existente = carrito.FirstOrDefault(d => d.CodigoLibro == codigoInterno);
                cantidadSolicitada += existente != null ? existente.Cantidad : 0;

                int stockDisponible = libroBLL.ConsultarStock(codigoInterno);
                if (cantidadSolicitada <= 0)
                    throw new Exception($"{LanguageManager.Instance.GetTraduction("msjCantidadInvalida")}");
                if (cantidadSolicitada > stockDisponible)
                    throw new Exception(
                        $"{LanguageManager.Instance.GetTraduction("StockInsuficiente")}: {stockDisponible}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public decimal CalcularTotal(List<DetalleVenta> detalle, string medioPago, int descuentoAplicado, out decimal descuentoTotal)
        {
            try
            {
                decimal subtotal = detalle.Sum(d => d.Subtotal);
                if (descuentoAplicado < 0 || descuentoAplicado > 100)
                    throw new Exception($"{LanguageManager.Instance.GetTraduction("DescuentoInvalido")}");
                descuentoTotal = subtotal * (descuentoAplicado / 100m);

                subtotal -= descuentoTotal;

                if(medioPago == "Efectivo")
                {
                    decimal descuentoEfectivo = subtotal * 0.10m;

                    // Validamos que al restar este descuento el total siga siendo mayor o igual a cero
                    if (subtotal - descuentoEfectivo >= 0)
                    {
                        subtotal -= descuentoEfectivo;
                        descuentoTotal += descuentoEfectivo; // Opcional: sumamos este descuento al total reportado
                    }
                }

                return subtotal;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public void ConfirmarVenta(Venta venta)
        {
            try
            {
                if (venta.Detalle == null || venta.Detalle.Count == 0)
                    throw new Exception($"{LanguageManager.Instance.GetTraduction("msjErrorVentaCarrito")}");
                

                int nroVenta = ventaDAL.RegistrarVenta(venta);

                foreach (DetalleVenta detalle in venta.Detalle)
                {
                    libroBLL.DescontarStock(detalle.CodigoLibro, detalle.Cantidad);
                }

                _digitoVerificadorBLL.RecalcularDVH_Libro();
                _digitoVerificadorBLL.RecalcularDVH_Detalle();
                _digitoVerificadorBLL.RecalcularDVH_Venta();
                _digitoVerificadorBLL.RecalcularDVV_Libro();
                _digitoVerificadorBLL.RecalcularDVV_Detalle();
                _digitoVerificadorBLL.RecalcularDVV_Venta();

                Bitacora bitacora = new Bitacora();
                bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
                bitacora.Modulo = "Ventas";
                bitacora.Evento = "Venta exitosa";
                bitacora.Criticidad = 1;
                _bitacoraBLL.RegistrarEvento(bitacora);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void SerializarLibro(List<Libro> listaLibros, string fileName)
        {
            _serializacionService.SerializarLibros(listaLibros, fileName);

            Bitacora bitacora = new Bitacora();
            bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
            bitacora.Modulo = "Venta";
            bitacora.Evento = "Serializacion Exitosa";
            bitacora.Criticidad = 1;
            _bitacoraBLL.RegistrarEvento(bitacora);
        }

        public List<Libro> DeserializarLibros(string fileName)
        {
            List<Libro> libros = _serializacionService.DeserializarLibros(fileName);

            Bitacora bitacora = new Bitacora();
            bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
            bitacora.Modulo = "Venta";
            bitacora.Evento = "Deserializacion Exitosa";
            bitacora.Criticidad = 1;
            _bitacoraBLL.RegistrarEvento(bitacora);

            return libros;
        }
    }
}
