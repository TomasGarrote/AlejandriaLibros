using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class ClienteBLL
    {
        private readonly ClienteDAL clienteDAL = new ClienteDAL();
        private readonly BitacoraBLL _bitacoraBLL = new BitacoraBLL();

        private readonly DigitoVerificadorBLL _digitoVerificadorBLL = new DigitoVerificadorBLL();

        public List<Cliente> ListarClientes()
        {
            return clienteDAL.ListarClientes();
        }
        public Cliente RetornarCliente(string dni)
        {
            return clienteDAL.RetornarCliente(dni);
        }

        public void CrearCliente(Cliente cliente)
        {
            clienteDAL.RegistrarCliente(cliente);

            _digitoVerificadorBLL.RecalcularDVH_Cliente();
            _digitoVerificadorBLL.RecalcularDVV_Cliente();

            Bitacora bitacora = new Bitacora();
            bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
            bitacora.Modulo = "Cliente";
            bitacora.Evento = "Cliente creado";
            bitacora.Criticidad = 1;
            _bitacoraBLL.RegistrarEvento(bitacora);
        }
        public void ModificarCliente(Cliente cliente)
        {
            clienteDAL.ModificarCliente(cliente);

            _digitoVerificadorBLL.RecalcularDVH_Cliente();
            _digitoVerificadorBLL.RecalcularDVV_Cliente();

            Bitacora bitacora = new Bitacora();
            bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
            bitacora.Modulo = "Cliente";
            bitacora.Evento = "Cliente modificado";
            bitacora.Criticidad = 1;
            _bitacoraBLL.RegistrarEvento(bitacora);
        }
        public void BorrarCliente(string Dni)
        {
            clienteDAL.BorrarCliente(Dni);

            _digitoVerificadorBLL.RecalcularDVH_Cliente();
            _digitoVerificadorBLL.RecalcularDVV_Cliente();

            Bitacora bitacora = new Bitacora();
            bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
            bitacora.Modulo = "Cliente";
            bitacora.Evento = "Cliente Borrado";
            bitacora.Criticidad = 1;
            _bitacoraBLL.RegistrarEvento(bitacora);
        }
    }
}
