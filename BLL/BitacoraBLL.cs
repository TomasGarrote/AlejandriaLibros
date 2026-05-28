using DAL;
using Microsoft.Data.SqlClient;
using Servicios;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class BitacoraBLL
    {
        BitacoraDAL DAL;
        public BitacoraBLL()
        {
            DAL = new BitacoraDAL();
        }

        public void RegistrarEvento(Bitacora unEvento)
        {
            unEvento.Fecha = DateTime.Now;
            DAL.RegistrarEvento(unEvento);
        }

        public List<Bitacora> ListarEventos()
        {
            return DAL.ListarEventos();
        }

        public List<Bitacora> FiltrarEventos(string nombre, string apellido, string login, string modulo, string evento, DateTime desde, DateTime hasta, int? criticidad)
        {
            if (desde > hasta)
            {
                throw new Exception("La fecha de inicio no puede ser mayor a la fecha final.");
            }

            if (modulo == "Todos") modulo = null;

            return DAL.FiltrarEventos(nombre, apellido, login, modulo, evento, desde, hasta, criticidad);
        }

        public List<string> ObtenerLogins() => DAL.ObtenerLogins();

        public UsuarioBE ObtenerUsuarioPorLogin(string login) => DAL.ObtenerUsuarioPorLogin(login);
    }
}
