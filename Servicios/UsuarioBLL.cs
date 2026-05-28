using System;
using System.Collections.Generic;
using System.Text;

namespace Servicios
{
    public class UsuarioBLL
    {
        private readonly UsuarioDAL usuarioDAL;
        public UsuarioBLL()
        {
            usuarioDAL = new UsuarioDAL();
        }
    }
}
