using System;
using System.Collections.Generic;
using System.Text;

namespace Servicios
{
    public class DigitoVerificador
    {
        public static string GetHexa(string texto)
        {
            string hex = string.Concat(texto.Select(c => ((int)c).ToString("X2")));
            return hex;
        }
        public static void RecalcularDV()
        {

        }

        private bool CompararDV(string DVCalculado, string DVOriginal)
        {
            throw new NotImplementedException();
        }

        public static string CalcularDVH(string cadena)
        {
           return cadena = Encriptador.GetHash256(GetHexa(cadena));
        }

        public static string CalcularDVV(List<string> cadena)
        {
            string DVVcolumna = "";
            foreach (var item in cadena)
            {
                DVVcolumna += GetHexa(item);
            }
            return Encriptador.GetHash256(DVVcolumna);
        }
    }
}
