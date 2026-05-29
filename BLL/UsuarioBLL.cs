using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            if (!Regex.IsMatch(usuario.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) throw new Exception("El Formato Del Email Es Incorrecto");
            if (!Regex.IsMatch(usuario.DNI, @"^\d{8}$")) throw new Exception("El Formato Del DNI Es Incorrecto");
            if (!Regex.IsMatch(usuario.Nombre, @"^.{3,}$") || !Regex.IsMatch(usuario.Nombre, @"^.{3,}$"))
                throw new Exception("El Formato Del Nombre o Apellido Es Incorrecto");

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

        public void Modificar(string dNI, UsuarioBE usuarioBE)
        {
            UsuarioBE existente = usuarioDAL.BuscarUsuarioPorDNI(usuarioBE.DNI);
            if (existente != null && existente.DNI != dNI)
                throw new Exception("Ya Existe User Con Ese DNI");
            else
            {

                UsuarioBE repetido = usuarioDAL.ObtenerPorUsuario(usuarioBE.Username);
                if (repetido != null && repetido.DNI != dNI)
                    throw new Exception("E lNombre De Usuario Ya Esta En Uso");
                else
                {
                    usuarioDAL.Modificar(dNI, usuarioBE);
                    throw new Exception("ModificacionCompletaU");
                }

            }
        }

        public void DesbloquearUsuario(UsuarioBE usuarioBE)
        {
            try
            {
                UsuarioBE user = usuarioDAL.BuscarUsuarioPorDNI(usuarioBE.DNI);

                if (user.Bloqueado)
                {
                    string nuevaClave = Encriptador.EncriptarSHA256(user.DNI + user.Nombre);

                    usuarioDAL.DesbloquearUsuario(user.DNI, nuevaClave);

                    throw new Exception("UsuarioFueDesbloqueadoYClaveRestauradaU");
                }
                else
                {
                    throw new Exception("UsuarioNoEstaBloqueadoU");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
