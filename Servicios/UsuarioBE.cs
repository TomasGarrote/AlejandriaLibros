using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Servicios
{
    public class UsuarioBE
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

        public UsuarioBE() { }

        public UsuarioBE(string dNI, string nombre, string apellido, string nombreUsuario, string password, string email, bool bloqueado, bool activo, string nombre_rol)
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
    }
}
