using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL
{
    public class UsuarioBLL
    {
        private readonly UsuarioDAL usuarioDAL;
        public UsuarioBLL()
        {
            usuarioDAL = new UsuarioDAL();
        }

        public void RegistrarUsuario(UsuarioBE usuarioBE)
        {
            //try
            //{
            //    ValidarCaracteresUsuario(usuarioBE);
            //    if (usuarioDal._012IP_BuscarAUsuarioPorDNI(usuarioBE.DNI) == null)
            //    {
            //        usuarioDal._012IP_Registrar(usuarioBE);
            //        throw new Exception(LanguageManager.Instance.GetTraduction("UsuarioRegistradoExitosamenteU"));
            //    }
            //    else { throw new Exception(LanguageManager.Instance.GetTraduction("UsuarioExistenteU")); }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        private void ValidarCaracteresUsuario(UsuarioBE usuario)
        {

            //if (!Regex.IsMatch(usuario.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) throw new Exception(LanguageManager.Instance.GetTraduction("ElFormatoDelEmailEsIncorrectoU"));
            //if (!Regex.IsMatch(usuario.DNI, @"^\d{8}$")) throw new Exception(LanguageManager.Instance.GetTraduction("ElFormatoDelDNIEsIncorrectoU"));
            //if (!Regex.IsMatch(usuario.Nombre, @"^.{3,}$") || !Regex.IsMatch(usuario.Nombre, @"^.{3,}$"))
            //    throw new Exception(LanguageManager.Instance.GetTraduction("ElFormatoDelNomOApeEsIncorrectoU"));

        }
    }
}
