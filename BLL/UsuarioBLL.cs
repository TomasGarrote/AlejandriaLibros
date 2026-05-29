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
        private readonly BitacoraBLL bitacoraBLL;
        public UsuarioBLL()
        {
            usuarioDAL = new UsuarioDAL();
            bitacoraBLL = new BitacoraBLL();
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
                    Bitacora bitacora = new Bitacora();
                    bitacora.Login = SessionManager.Instance.UsuarioActual().ToString();
                    bitacora.Modulo = "Usuarios";
                    bitacora.Evento = "Crear usuario exitoso";
                    bitacora.Criticidad = 1;

                    bitacoraBLL.RegistrarEvento(bitacora);
                    throw new Exception("Usuario registrado exitosamente");
                }
                else
                {
                    throw new Exception("El usuario ya existe");
                }

            }
            catch (Exception ex)
            {
                throw ex;
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
                    Bitacora bitacora = new Bitacora();
                    bitacora.Login = SessionManager.Instance.UsuarioActual().ToString();
                    bitacora.Modulo = "Usuarios";
                    bitacora.Evento = "Eliminar Usuario (Baja lógica) exitoso";
                    bitacora.Criticidad = 1;

                    bitacoraBLL.RegistrarEvento(bitacora);
                }
                else
                {
                    usuarioDAL.ActivarUsuario(dNI);
                    Bitacora bitacora = new Bitacora();
                    bitacora.Login = SessionManager.Instance.UsuarioActual().ToString();
                    bitacora.Modulo = "Usuarios";
                    bitacora.Evento = "Eliminar Usuario (Baja lógica) exitoso";
                    bitacora.Criticidad = 1;

                    bitacoraBLL.RegistrarEvento(bitacora);
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
                Bitacora bitacora = new Bitacora();
                bitacora.Login = SessionManager.Instance.UsuarioActual().ToString();
                bitacora.Modulo = "Usuarios";
                bitacora.Evento = "Cambiar Clave exitoso";
                bitacora.Criticidad = 1;
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

                UsuarioBE repetido = usuarioDAL.ObtenerPorUserName(usuarioBE.Username);
                if (repetido != null && repetido.DNI != dNI)
                    throw new Exception("E Nombre De Usuario Ya Esta En Uso");
                else
                {
                    usuarioDAL.Modificar(dNI, usuarioBE);
                    Bitacora bitacora = new Bitacora();
                    bitacora.Login = SessionManager.Instance.UsuarioActual().ToString();
                    bitacora.Modulo = "Usuarios";
                    bitacora.Evento = "Modificar Usuario exitoso";
                    bitacora.Criticidad = 1;
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
                    string nuevaClave = Encriptador.GetHash256(user.DNI + user.Nombre);

                    usuarioDAL.DesbloquearUsuario(user.DNI, nuevaClave);
                    Bitacora bitacora = new Bitacora();
                    bitacora.Login = SessionManager.Instance.UsuarioActual().ToString();
                    bitacora.Modulo = "Usuarios";
                    bitacora.Evento = "Desbloquear Usuario exitoso";
                    bitacora.Criticidad = 1;
                }
                else
                {
                    throw new Exception("Usuario No Esta Bloqueado");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UsuarioBE> ListarTodosUsuarios()=>usuarioDAL.ListarTodosLosUsuarios();

        public UsuarioBE BuscarUsuarioPorDNI(string v) => usuarioDAL.BuscarUsuarioPorDNI(v);
    }
}
