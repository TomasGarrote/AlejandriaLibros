using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class LibroBLL
    {
        LibroDAL libroDAL = new LibroDAL();

        BitacoraBLL _bitacoraBLL = new BitacoraBLL();

        public List<Libro> ListarDisponibles()
        {
            return libroDAL.ListarDisponibles();
        }

        public int ConsultarStock(int codigoInterno)
        {
            return libroDAL.ConsultarStock(codigoInterno);
        }

        public void DescontarStock(int codigoInterno, int cantidad)
        {
            libroDAL.DescontarStock(codigoInterno, cantidad);

            Bitacora bitacora = new Bitacora();
            bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
            bitacora.Modulo = "Stock";
            bitacora.Evento = "Se desconto el stock del producto exitosamente";
            bitacora.Criticidad = 1;
            _bitacoraBLL.RegistrarEvento(bitacora);
        }

        public List<Libro> ListarLibrosPorFiltro(string categoria, string autor, string titulo, string isbn, string ubicacion)
        {
            return libroDAL.ListarLibrosPorFiltro(categoria, autor, titulo, isbn, ubicacion);
        }

        public object [] ListarAutores()
        {
            return libroDAL.ListarAutores();
        }

        public object [] ListarCategorias()
        {
            return libroDAL.ListarCategorias();
        }
    }
}
