using System;
using System.IO;

namespace Servicios
{
    public static class ConexionConfig
    {
        private static readonly string CarpetaConfig =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AlejandriaLibros");

        private static readonly string ArchivoConfig =
            Path.Combine(CarpetaConfig, "conexion.cfg");

        public static bool ExisteConfiguracion()
        {
            return File.Exists(ArchivoConfig);
        }

        public static void GuardarCadenaConexion(string cadenaConexion)
        {
            Directory.CreateDirectory(CarpetaConfig);
            File.WriteAllText(ArchivoConfig, cadenaConexion);
        }

        public static string ObtenerCadenaConexion()
        {
            if (!ExisteConfiguracion())
                throw new Exception("No hay una configuración de conexión guardada.");

            return File.ReadAllText(ArchivoConfig);
        }

        public static void EliminarConfiguracion()
        {
            if (File.Exists(ArchivoConfig))
                File.Delete(ArchivoConfig);
        }
    }
}