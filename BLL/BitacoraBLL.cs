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
                throw new Exception(LanguageManager.Instance.GetTraduction("BitBlltext1"), ex);
            }
        }


        public List<Bitacora> FiltrarEventos(string nombre, string apellido, string login, string modulo, string evento, DateTime desde, DateTime hasta, int? criticidad)
        {
            try
            {
                if (desde > hasta)
                    throw new Exception(LanguageManager.Instance.GetTraduction("BitBlltext2"));

                if (modulo == "Todos") modulo = null;

                return DAL.FiltrarEventos(nombre, apellido, login, modulo, evento, desde, hasta, criticidad);
            }
            catch (Exception ex)
            {
                throw new Exception(LanguageManager.Instance.GetTraduction("BitBlltext3"), ex);
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
                throw new Exception(LanguageManager.Instance.GetTraduction("BitBlltext4"), ex);
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
                throw new Exception(LanguageManager.Instance.GetTraduction("BitBlltext5"), ex);
            }
        }
    }
}