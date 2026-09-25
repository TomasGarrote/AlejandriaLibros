using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class VentaBLL
    {
        private readonly LibroBLL libroBLL = new LibroBLL();
        private readonly VentaDAL ventaDAL = new VentaDAL();
        private readonly BitacoraBLL _bitacoraBLL = new BitacoraBLL();
        private readonly DigitoVerificadorBLL _digitoVerificadorBLL = new DigitoVerificadorBLL();

        public List<Libro> ObtenerCatalogo()
        {
            try
            {
                var lista = libroBLL.ListarDisponibles();
                if (lista.Count == 0)
                    throw new Exception("No existen ejemplares disponibles en el momento");
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
                if (lista.Count == 0)
                    throw new Exception("No se encontraron resultados para los filtros aplicados");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ValidarAgregarAlCarrito(int codigoInterno, int cantidadSolicitada)
        {
            try
            {
                int stockDisponible = libroBLL.ConsultarStock(codigoInterno);
                if (cantidadSolicitada <= 0)
                    throw new Exception("La cantidad debe ser mayor a cero");
                if (cantidadSolicitada > stockDisponible)
                    throw new Exception(
                        $"La operación no puede completarse por falta de stock. Stock disponible: {stockDisponible}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public decimal CalcularTotal(List<DetalleVenta> detalle, decimal descuento, out decimal subtotal)
        {
            try
            {
                subtotal = detalle.Sum(d => d.Subtotal);
                if (descuento < 0 || descuento > subtotal)
                    throw new Exception("El descuento ingresado no es válido");
                return subtotal - descuento;
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
                    throw new Exception("No hay productos cargados en la venta");
                foreach (DetalleVenta detalle in venta.Detalle)
                {
                    libroBLL.DescontarStock(detalle.CodigoLibro, detalle.Cantidad);
                }
                ventaDAL.RegistrarVenta(venta);
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
    }
}
