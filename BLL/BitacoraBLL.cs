using DAL;
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
            try
            {
                unEvento.Fecha = DateTime.Now;
                DAL.RegistrarEvento(unEvento);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar el evento en la bitácora.", ex);
            }
        }


        public List<Bitacora> FiltrarEventos(string nombre, string apellido, string login, string modulo, string evento, DateTime desde, DateTime hasta, int? criticidad)
        {
            try
            {
                if (desde > hasta)
                    throw new Exception("La fecha de inicio no puede ser mayor a la fecha final.");

                if (modulo == "Todos") modulo = null;

                return DAL.FiltrarEventos(nombre, apellido, login, modulo, evento, desde, hasta, criticidad);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudieron filtrar los eventos.", ex);
            }
        }

        public List<string> ObtenerLogins()
        {
            try
            {
                return DAL.ObtenerLogins();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudieron obtener los usuarios.", ex);
            }
        }

        public Usuario ObtenerUsuarioPorLogin(string login)
        {
            try
            {
                return DAL.ObtenerUsuarioPorLogin(login);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener el usuario.", ex);
            }
        }
    }
}