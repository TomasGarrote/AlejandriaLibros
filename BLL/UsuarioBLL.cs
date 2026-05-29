using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
                SessionManager.Instance.Logueado();
                if(usuarioDAL == null)
                {
                    throw new Exception("No se pudo conectar a la base de datos.");
                }
                UsuarioBE usuarioBE = usuarioDAL.ObtenerPorUserName(usuario);
                if (usuarioBE != null)
                {
                    if (usuarioBE.Bloqueado) return LoginResultado.Bloqueado;

                    if(usuarioBE.Password != Encriptador.GetHash256(contraseña))
                    {
                        usuarioDAL.SumarIntentoFallido(usuarioBE);

                        if (usuarioDAL.ObtenerIntentosFallidos(usuarioBE.DNI) >= 3)
                        {
                            usuarioDAL.BloquearUsuario(usuarioBE.Username);
                            return LoginResultado.Bloqueado;
                        }
                        return LoginResultado.ContraseñaIncorrecta;
                    }
                    else
                    {
                        SessionManager.Instance.Loguear(usuarioBE.Username);
                        //usuarioDAL.ResetearIntentos(usuarioBE);
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
                SessionManager.Instance.Desloguear();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw new Exception(ex.Message);
            }
        }

        public void RegistrarUsuario(UsuarioBE usuarioBE)
        {
            try
            {
                ValidarCaracteresUsuario(usuarioBE);
                if (usuarioDAL.BuscarUsuarioPorDNI(usuarioBE.DNI) == null)
                {
                    usuarioDAL.Registrar(usuarioBE);
                    throw new Exception("Usuario registrado exitosamente");
                }
                else
                {
                    throw new Exception("El usuario ya existe");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ValidarCaracteresUsuario(UsuarioBE usuario)
        {

            if (!Regex.IsMatch(usuario.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) throw new Exception("ElFormatoDelEmailEsIncorrectoU");
            if (!Regex.IsMatch(usuario.DNI, @"^\d{8}$")) throw new Exception("ElFormatoDelDNIEsIncorrectoU");
            if (!Regex.IsMatch(usuario.Nombre, @"^.{3,}$") || !Regex.IsMatch(usuario.Nombre, @"^.{3,}$"))
                throw new Exception("ElFormatoDelNomOApeEsIncorrectoU");

        }

        public List<UsuarioBE> ListarUsuariosActivos()
        {
            var todos = usuarioDAL.ListarTodosLosUsuarios();

            var activos = todos.Where(u => u.Activo).ToList();

            return activos as List<UsuarioBE>;
        }

        public void EliminarLogico(string dNI)
        {
            try
            {
                UsuarioBE user = usuarioDAL.BuscarUsuarioPorDNI(dNI);

                if (user.Activo)
                {
                    usuarioDAL.EliminarLogico(dNI);
                    throw new Exception("Usuario Fue Eliminado");

                }
                else
                {
                    throw new Exception("El Usuario Ya Esta Eliminado");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void CambiarClave(string usuario, string nuevaContra)
        {
            try
            {
                UsuarioBE user = usuarioDAL.ObtenerPorUserName(usuario);
                if(user.Password == Encriptador.GetHash256(nuevaContra))
                {
                    throw new Exception("La nueva contraseña no puede ser igual a la anterior");
                }
                usuarioDAL.CambiarClave(usuario, Encriptador.GetHash256(nuevaContra));
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
