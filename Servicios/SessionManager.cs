namespace Servicios
{
    public class SessionManager
    {
        private static SessionManager _instance;

       
        private Usuario _usuarioLogueado;

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

       
        public void Loguear(Usuario usuario)
        {
            if (Logueado())
            {
                throw new Exception(LanguageManager.Instance.GetTraduction("UserLogueado"));
            }
            _usuarioLogueado = usuario;
        }

        public void Desloguear()
        {
            if (!Logueado())
            {
                throw new Exception(LanguageManager.Instance.GetTraduction("UserNoLogueado"));
            }
            _usuarioLogueado = null;
        }

        public bool Logueado()
        {
            return _usuarioLogueado != null;
        }

        public Usuario UsuarioActual()
        {
            if (!Logueado())
            {
                throw new Exception(LanguageManager.Instance.GetTraduction("UserNoLogueado"));
            }
            return _usuarioLogueado;
        }
    }
}