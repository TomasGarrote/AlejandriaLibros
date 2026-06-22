using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
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
                if (SessionManager.Instance.Logueado())
                {
                    throw new Exception("Ya hay un usuario logueado en el sistema.");
                }

                if (usuarioDAL == null)
                {
                    throw new Exception("No se pudo conectar a la base de datos.");
                }

                Usuario usuarioBE = usuarioDAL.ObtenerPorUserName(usuario);
                if (usuarioBE != null)
                {
                    if (usuarioBE.Bloqueado) return LoginResultado.Bloqueado;

                    if (usuarioBE.Password != Encriptador.GetHash256(contraseña))
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
                    
                        BLL.PerfilBLL perfilBLL = new BLL.PerfilBLL();
                        var todosLosPerfiles = perfilBLL.ObtenerPerfiles();
                        var perfilUsuario = todosLosPerfiles.FirstOrDefault(p => p.Nombre == usuarioBE.Rol);

                        if (perfilUsuario != null)
                        {
                            usuarioBE.Permisos.Add(perfilUsuario);
                        }

                        SessionManager.Instance.Loguear(usuarioBE);

                        Bitacora bitacora = new Bitacora();
                        bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
                        bitacora.Modulo = "Usuarios";
                        bitacora.Evento = "Login exitoso";
                        bitacora.Criticidad = 1;
                        bitacoraBLL.RegistrarEvento(bitacora);

                        return LoginResultado.Valido;
                    }
                }
                return LoginResultado.UsuarioNoEncontrado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Desloguear()
        {
            try
            {
                Bitacora bitacora = new Bitacora();
                bitacora.Login = SessionManager.Instance.UsuarioActual().Username;

                SessionManager.Instance.Desloguear();

                bitacora.Modulo = "Usuarios";
                bitacora.Evento = "Desloguear usuario";
                bitacora.Criticidad = 1;
                bitacoraBLL.RegistrarEvento(bitacora);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RegistrarUsuario(Usuario usuarioBE)
        {
            try
            {
                ValidarCaracteresUsuario(usuarioBE);
                if (usuarioDAL.BuscarUsuarioPorDNI(usuarioBE.DNI) == null)
                {
                    usuarioDAL.Registrar(usuarioBE);

                    Bitacora bitacora = new Bitacora();
                    bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
                    bitacora.Modulo = "Usuarios";
                    bitacora.Evento = "Crear usuario exitoso";
                    bitacora.Criticidad = 1;
                    bitacoraBLL.RegistrarEvento(bitacora);

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

        private void ValidarCaracteresUsuario(Usuario usuario)
        {
            if (!Regex.IsMatch(usuario.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) throw new Exception("El Formato Del Email Es Incorrecto");
            if (!Regex.IsMatch(usuario.DNI, @"^\d{8}$")) throw new Exception("El Formato Del DNI Es Incorrecto");
            if (!Regex.IsMatch(usuario.Nombre, @"^.{3,}$") || !Regex.IsMatch(usuario.Apellido, @"^.{3,}$"))
                throw new Exception("El Formato Del Nombre o Apellido Es Incorrecto");
        }

        public List<Usuario> ListarUsuariosActivos()
        {
            var todos = usuarioDAL.ListarTodosLosUsuarios();
            var activos = todos.Where(u => u.Activo).ToList();
            return activos;
        }

        public void EliminarLogico(string dNI)
        {
            try
            {
                Usuario user = usuarioDAL.BuscarUsuarioPorDNI(dNI);

                if (user.Activo)
                {
                    usuarioDAL.EliminarLogico(dNI);
                    Bitacora bitacora = new Bitacora();
                    bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
                    bitacora.Modulo = "Usuarios";
                    bitacora.Evento = "Eliminar Usuario (Baja lógica) exitoso";
                    bitacora.Criticidad = 1;
                    bitacoraBLL.RegistrarEvento(bitacora);
                }
                else
                {
                    usuarioDAL.ActivarUsuario(dNI);
                    Bitacora bitacora = new Bitacora();
                    bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
                    bitacora.Modulo = "Usuarios";
                    bitacora.Evento = "Activar Usuario (Baja lógica) exitoso";
                    bitacora.Criticidad = 1;
                    bitacoraBLL.RegistrarEvento(bitacora);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LoginResultado CambiarClave(string usuario, string contraActual, string nuevaContra)
        {
            try
            {
                Usuario user = usuarioDAL.ObtenerPorUserName(usuario);

                if (user == null) return LoginResultado.UsuarioNoEncontrado;
                if (user.Password == Encriptador.GetHash256(nuevaContra)) return LoginResultado.ContraseñaIguales;
                if (user.Password != Encriptador.GetHash256(contraActual)) return LoginResultado.ContraseñaIncorrecta;
                if (user.Bloqueado) return LoginResultado.Bloqueado;

                usuarioDAL.CambiarClave(usuario, Encriptador.GetHash256(nuevaContra));

                Bitacora bitacora = new Bitacora();
                bitacora.Login = usuario;
                bitacora.Modulo = "Usuarios";
                bitacora.Evento = "Cambiar Clave exitoso";
                bitacora.Criticidad = 1;
                bitacoraBLL.RegistrarEvento(bitacora);

                return LoginResultado.Valido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(string dNI, Usuario usuarioBE)
        {
            Usuario existente = usuarioDAL.BuscarUsuarioPorDNI(usuarioBE.DNI);
            if (existente != null && existente.DNI != dNI)
                throw new Exception("Ya Existe User Con Ese DNI");
            else
            {
                Usuario repetido = usuarioDAL.ObtenerPorUserName(usuarioBE.Username);
                if (repetido != null && repetido.DNI != dNI)
                    throw new Exception("El Nombre De Usuario Ya Esta En Uso");
                else
                {
                    usuarioDAL.Modificar(dNI, usuarioBE);

                    Bitacora bitacora = new Bitacora();
                    bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
                    bitacora.Modulo = "Usuarios";
                    bitacora.Evento = "Modificar Usuario exitoso";
                    bitacora.Criticidad = 1;
                    bitacoraBLL.RegistrarEvento(bitacora);
                }
            }
        }

        public void DesbloquearUsuario(Usuario usuarioBE)
        {
            try
            {
                Usuario user = usuarioDAL.BuscarUsuarioPorDNI(usuarioBE.DNI);

                if (user.Bloqueado)
                {
                    string nuevaClave = Encriptador.GetHash256(user.DNI + user.Nombre);

                    usuarioDAL.DesbloquearUsuario(user.DNI, nuevaClave);

                    Bitacora bitacora = new Bitacora();
                    bitacora.Login = SessionManager.Instance.UsuarioActual().Username;
                    bitacora.Modulo = "Usuarios";
                    bitacora.Evento = "Desbloquear Usuario exitoso";
                    bitacora.Criticidad = 1;
                    bitacoraBLL.RegistrarEvento(bitacora);
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

        public List<Usuario> ListarTodosUsuarios() => usuarioDAL.ListarTodosLosUsuarios();
        public Usuario BuscarUsuarioPorDNI(string v) => usuarioDAL.BuscarUsuarioPorDNI(v);
        public Usuario BuscarUsuarioPorUserName(string user) => usuarioDAL.ObtenerPorUserName(user);

        public bool ValidarNuevoUsuario(string user, string contra)
        {
            Usuario usuario = usuarioDAL.ObtenerPorUserName(user);
            if (usuario != null)
            {
                if (usuario.Password == Encriptador.GetHash256(usuario.DNI + usuario.Nombre))
                {
                    if (Encriptador.GetHash256(contra) == usuario.Password)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}