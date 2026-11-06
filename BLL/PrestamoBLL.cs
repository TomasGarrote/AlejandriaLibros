using System;
using System.Collections.Generic;
using System.Text;
using BE;   
using DAL;
using Servicios;

namespace BLL
{
    public class PrestamoBLL
    {
        private readonly LibroBLL libroBLL = new LibroBLL();
        private readonly PrestamosDAL _prestamosDAL = new PrestamosDAL();
        private readonly BitacoraBLL _bitacoraBLL = new BitacoraBLL();
        private readonly DigitoVerificadorBLL _digitoVerificadorBLL = new DigitoVerificadorBLL();

        public DateTime CalcularFechaDevolucion(int dias)
        {
            return DateTime.Now.AddDays(dias);
        }

        public decimal CalcularMultaMora(DateTime fechaDevolucion, DateTime fechaActual)
        {
            decimal multaDiaria = 10; // Valor de la multa diaria

            if (fechaActual > fechaDevolucion)
            {
                int diasAtraso = (fechaActual - fechaDevolucion).Days;
                return diasAtraso * multaDiaria;
            }
            return 0;
        }

        public void RegistrarPrestamo(Prestamo prestamo)
        {
            _prestamosDAL.RegistrarPrestamo(prestamo);
            libroBLL.DescontarStock(Convert.ToInt32(prestamo.CodigoInterno),1);

            _digitoVerificadorBLL.RecalcularDVH_Prestamo();
            _digitoVerificadorBLL.RecalcularDVV_Prestamo();
            _digitoVerificadorBLL.RecalcularDVH_Libro();
            _digitoVerificadorBLL.RecalcularDVV_Libro();

            // Registrar evento en la bitácora
            Bitacora bitacora = new Bitacora();
            bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
            bitacora.Modulo = "Prestamo";
            bitacora.Evento = "Prestamo registrado";
            bitacora.Criticidad = 1;
            _bitacoraBLL.RegistrarEvento(bitacora);
        }

        public void RegistrarDevolucion(Prestamo prestamo)
        {
            try
            {
                prestamo.FechaDevolucionReal = DateTime.Now;

                ValidarFechaDevolucion(prestamo);

                prestamo.Estado = "Terminado";

                _prestamosDAL.RegistrarDevolucion(prestamo);
                libroBLL.SumarStock(Convert.ToInt32(prestamo.CodigoInterno),1);

                _digitoVerificadorBLL.RecalcularDVH_Prestamo();
                _digitoVerificadorBLL.RecalcularDVV_Prestamo();
                _digitoVerificadorBLL.RecalcularDVH_Libro();
                _digitoVerificadorBLL.RecalcularDVV_Libro();
                // Registrar evento en la bitácora
                Bitacora bitacora = new Bitacora();
                bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
                bitacora.Modulo = "Prestamo";
                bitacora.Evento = "Devolución registrada";
                bitacora.Criticidad = 1;
                _bitacoraBLL.RegistrarEvento(bitacora);
            }
            catch (Exception ex)
            {

                throw ex;
            }


            
        }

        public List<Prestamo> ConsultarHistorial(string dniCliente,string estado, string titulo, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                List<Prestamo> prestamos = new List<Prestamo>();
                if (fechaDesde > fechaHasta)
                {
                    throw new Exception($"{LanguageManager.Instance.GetTraduction("msjErrorPrestamoFechaDesdeMayor")}");
                }
                if (_prestamosDAL.ListarPrestamos(dniCliente, estado, titulo, fechaDesde, fechaHasta).Count > 0)
                {
                    prestamos = _prestamosDAL.ListarPrestamos(dniCliente, estado, titulo, fechaDesde, fechaHasta);
                }

                return prestamos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public Prestamo RetornarPrestamo(int nroPrestamo)
        {
            
            return _prestamosDAL.RetornarPrestamo(nroPrestamo);
        }

        public void ValidarFechaDevolucion(Prestamo prestamo)
        {
            if (prestamo.FechaDevolucionReal > prestamo.FechaProgramada)
            {
                throw new Exception($"{LanguageManager.Instance.GetTraduction("msjErrorPrestamoFechaDevolucion")}");
            }
        }

        public void ValidarHabilitacionCliente(int dniCliente)
        {
            List<Prestamo> prestamos = ListarPrestamosCliente(dniCliente); // Llamada a la función para obtener los préstamos del cliente
            // Aquí podrías verificar si el cliente está habilitado basado en sus préstamos
            if (prestamos == null || prestamos.Count == 0)
            {
                throw new Exception($"{LanguageManager.Instance.GetTraduction("msjErrorPrestamoClienteInhabilitado")}");
            }
        }

        public List<Prestamo> ListarPrestamosCliente(int dniCliente)
        {
            

            return _prestamosDAL.ListarPrestamosCliente(dniCliente); ; // Retorna una lista vacía como ejemplo
        }

       public List<Prestamo> ListarPrestamosPendientes()
        {
            return _prestamosDAL.ListarPrestamosPendientes("Pendiente");
        }

        public object[] ListarEstados()
        {
            return _prestamosDAL.ListarEstados();
        }

        public IEnumerable<Prestamo> ListarTodosLosPrestamos()
        {
            return _prestamosDAL.ListarTodosLosPrestamos();
        }
    }
}
