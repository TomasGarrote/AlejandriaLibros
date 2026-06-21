using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class IdiomaBLL
    {
        private readonly IdiomaDAL _idiomaDAL;

        public IdiomaBLL()
        {
            _idiomaDAL = new IdiomaDAL();
        }

        public string ObtenerIdioma(string userName)
        {
            return _idiomaDAL.ObtenerIdioma(userName);
        }

        public void GuardarIdioma(string userName, string idioma)
        {
            _idiomaDAL.GuardarIdioma(userName, idioma);
        }
    }
}
