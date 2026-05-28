using System;
using System.Collections.Generic;
using System.Text;

namespace Servicios
{
    public class SessionManager
    {
        private static SessionManager _instance;
        private string _usuarioLogueado;
        private SessionManager()
        {
            _usuarioLogueado = null;
        }
        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SessionManager();
                }
                return _instance;
            }
        }
        public void Loguear(string usuario)
        {
            if (Logueado())
            {
                throw new Exception("Ya hay un usuario logueado.");
            }
            _usuarioLogueado = usuario;
        }
        public void Desloguear()
        {
            if (!Logueado())
            {
                throw new Exception("No hay ningún usuario logueado.");
            }
            _usuarioLogueado = null;
        }
        public bool Logueado()
        {
            return _usuarioLogueado != null;
        }
        public string UsuarioActual()
        {
            if (!Logueado())
            {
                throw new Exception("No hay ningún usuario logueado.");
            }
            return _usuarioLogueado;
        }
    }
}
