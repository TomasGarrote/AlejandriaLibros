using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Servicios
{
    public class Usuario
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Username { get; set; }
        [Browsable(false)]
        public string Password { get; set; }
        public string Email { get; set; }
        [Browsable(false)]
        public bool Bloqueado { get; set; }
        [Browsable(false)]
        public bool Activo { get; set; }
        public string Rol { get; set; }
        public int Intentos { get; set; }
        public string DVH { get; set; }

        public Usuario() { }
   
        public Usuario(string dNI, string nombre, string apellido, string nombreUsuario, string password, string email, bool bloqueado, bool activo, string nombre_rol)
        {
            DNI = dNI;
            Nombre = nombre;
            Apellido = apellido;
            Username = nombreUsuario;
            Password = password.Trim();
            Email = email;
            Bloqueado = bloqueado;
            Activo = activo;
            Rol = nombre_rol;
        }
        public Usuario(string dNI, string nombre, string apellido, string nombreUsuario, string password, string email, bool bloqueado, bool activo, string nombre_rol, int intentos)
        {
            DNI = dNI;
            Nombre = nombre;
            Apellido = apellido;
            Username = nombreUsuario;
            Password = password.Trim();
            Email = email;
            Bloqueado = bloqueado;
            Activo = activo;
            Rol = nombre_rol;
            Intentos = intentos;
        }
        public Usuario(string dNI, string nombre, string apellido, string nombreUsuario, string password, string email, bool bloqueado, bool activo, string nombre_rol, int intentos, string dvh)
        {
            DNI = dNI;
            Nombre = nombre;
            Apellido = apellido;
            Username = nombreUsuario;
            Password = password.Trim();
            Email = email;
            Bloqueado = bloqueado;
            Activo = activo;
            Rol = nombre_rol;
            Intentos = intentos;
            DVH = dvh;
        }

        [Browsable(false)]
        public List<ComponentePermiso> Permisos { get; set; } = new List<ComponentePermiso>();
        public bool TienePermiso(string nombrePermiso)
        {
            if (Permisos == null) return false;

            foreach (var componente in Permisos)
            {
              
                if (componente.TienePermiso(nombrePermiso))
                    return true;
            }
            return false;
        }
    }
}
