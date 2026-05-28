using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL
{
    public class UsuarioBLL
    {
        private readonly UsuarioDAL usuarioDAL;
        public UsuarioBLL()
        {
            usuarioDAL = new UsuarioDAL();
        }
        public LoginResultado Login(string usuario, string contraseña)
        {
			try
			{
                ManagerDeSesion.Instance.Logueado();
                if(usuarioDAL == null)
                {
                    throw new Exception("No se pudo conectar a la base de datos.");
                }
                UsuarioBE usuarioBE = usuarioDAL.ObtenerPorUsuario(usuario);
                if (usuarioBE != null)
                {
                    if (usuarioBE.Bloqueado) return LoginResultado.Bloqueado;

                    if(usuarioBE.Password != Encriptador.DesencriptarAES(contraseña))
                    {
                        usuarioDAL.SumarIntento(usuarioBE.Username);

                        if (usuarioDAL.ObtenerIntentosFallidos(usuarioBE.DNI) >= 3)
                        {
                            usuarioDAL.BloquearUsuario(usuarioBE.Username);
                            return LoginResultado.Bloqueado;
                        }
                        return LoginResultado.ContraseñaIncorrecta;
                    }
                    else
                    {
                        ManagerDeSesion.Instance.Loguear(usuarioBE.Username);
                        usuarioDAL.ResetearIntentos(usuarioBE.Username);
                        return LoginResultado.Valido;
                    }
                }
                return LoginResultado.UsuarioNoEncontrado;
            }
			catch (Exception ex)
			{
                ex.Message.ToString();
                throw new Exception(ex.Message);
            }
        }

        public void Desloguear()
        {
            try
            {
                ManagerDeSesion.Instance.Desloguear();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw new Exception(ex.Message);
            }
        }
    }
}
