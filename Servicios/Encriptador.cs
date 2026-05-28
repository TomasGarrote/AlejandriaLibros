using System.Security.Cryptography;
using System.Text;

namespace Servicios
{
    public class Encriptador
    {
        //IRREVERSIBLE
        public static string EncriptarSHA256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytesTexto = Encoding.UTF8.GetBytes(texto);
                byte[] hash = sha256.ComputeHash(bytesTexto);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        // REVERSIBLE 
        private static readonly string Key = "12345678910111213141516171819202";
        private static readonly string IV = "1234567891011121";

        public static string EncriptarAES(string texto)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);
                ICryptoTransform encriptador = aes.CreateEncryptor();
                byte[] bytesTexto = Encoding.UTF8.GetBytes(texto);
                byte[] bytesEncriptados = encriptador.TransformFinalBlock(bytesTexto, 0, bytesTexto.Length);
                return Convert.ToBase64String(bytesEncriptados);
            }
        }

        public static string DesencriptarAES(string textoEncriptado)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);
                ICryptoTransform desencriptador = aes.CreateDecryptor();
                byte[] bytesEncriptados = Convert.FromBase64String(textoEncriptado);
                byte[] bytesTexto = desencriptador.TransformFinalBlock(bytesEncriptados, 0, bytesEncriptados.Length);
                return Encoding.UTF8.GetString(bytesTexto);
            }
        }
    }
}
