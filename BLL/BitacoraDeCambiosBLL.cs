using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BitacoraDeCambiosBLL
    {
        private readonly ClienteHistorialDAL _dal;
        private readonly ClienteBLL _clienteBLL;
        private readonly DigitoVerificadorBLL _digitoVerificadorBLL;
        private readonly BitacoraBLL _bitacoraBLL;

        public BitacoraDeCambiosBLL()
        {
            _dal = new ClienteHistorialDAL();
            _bitacoraBLL = new BitacoraBLL();
            _clienteBLL = new ClienteBLL();
            _digitoVerificadorBLL = new DigitoVerificadorBLL();
        }

        public List<ClienteHistorial> ListarTodoElHistorial()
        {
            try
            {
                return _dal.ListarTodoElHistorial();
            }
            catch (Exception ex)
            {
                throw new Exception(LanguageManager.Instance.GetTraduction("ClienteHistError1") ?? "Error al listar el historial de clientes.", ex);
            }
        }
        public List<ClienteHistorial> FiltrarHistorial(int? dni, string nombre, string apellido, DateTime? desde, DateTime? hasta)
        {
            try
            {
                return _dal.FiltrarHistorial(dni, nombre, apellido, desde, hasta);
            }
            catch (Exception ex)
            {
                throw new Exception(LanguageManager.Instance.GetTraduction("ClienteHistError2") ?? "Error al filtrar el historial de clientes.", ex);
            }
        }

        public List<ClienteHistorial> ObtenerHistorialPorCliente(int dni)
        {
            try
            {
                if (dni <= 0)
                {
                    throw new ArgumentException("El DNI ingresado no es válido.");
                }

                return _dal.ObtenerHistorialPorCliente(dni);
            }
            catch (Exception ex)
            {
                throw new Exception(LanguageManager.Instance.GetTraduction("ClienteHistError3") ?? "Error al obtener el historial del cliente especificado.", ex);
            }
        }

        public void ActualizarDVH(int idHistorial, string dvhCalculado)
        {
            try
            {
                if (idHistorial <= 0)
                {
                    throw new ArgumentException("El ID de historial no es válido.");
                }

                _dal.ActualizarDVH(idHistorial, dvhCalculado);
            }
            catch (Exception ex)
            {
                throw new Exception(LanguageManager.Instance.GetTraduction("ClienteHistError4") ?? "Error al actualizar el DVH del historial de clientes.", ex);
            }
        }

        public void ActivarVersionCliente(ClienteHistorial historialSeleccionado, string usuarioLogueado)
        {
            int DniHistorialSeleccionado = historialSeleccionado.DNI;
            ClienteHistorial historialExistente = _dal.ObtenerVersionHistorial(DniHistorialSeleccionado); 

            Cliente clienteActualizado = new Cliente
            {
                DNI = DniHistorialSeleccionado,
                Nombre = historialSeleccionado.Nombre,
                Apellido = historialSeleccionado.Apellido,
                Email = historialSeleccionado.Email,
                Telefono = historialSeleccionado.Telefono
            };

            _clienteBLL.ActivarVersionCliente(clienteActualizado, usuarioLogueado);


            Bitacora bitacora = new Bitacora();
            bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
            bitacora.Modulo = "Cliente";
            bitacora.Evento = $"Activación de versión del cliente: {historialExistente.DNI}";
            bitacora.Criticidad = 1;
            _bitacoraBLL.RegistrarEvento(bitacora);

            _digitoVerificadorBLL.RecalcularDVH_Cliente();
            _digitoVerificadorBLL.RecalcularDVV_Cliente();
            //_digitoVerificadorBLL.RecalcularDVH_ClienteHistorial();
            //_digitoVerificadorBLL.RecalcularDVV_ClienteHistorial();
        }
    }
}
