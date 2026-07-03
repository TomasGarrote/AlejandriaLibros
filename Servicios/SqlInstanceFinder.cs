using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    public static class SqlInstanceFinder
    {
        public static List<string> ObtenerInstancias()
        {
            List<string> lista = new List<string>();

            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL"))
                {
                    if (key != null)
                    {
                        foreach (string instancia in key.GetValueNames())
                        {
                            if (instancia.Equals("MSSQLSERVER",
                                StringComparison.OrdinalIgnoreCase))
                            {
                                lista.Add(Environment.MachineName);
                            }
                            else
                            {
                                lista.Add(
                                    $"{Environment.MachineName}\\{instancia}");
                            }
                        }
                    }
                }
            }
            catch
            {
                // Ignorar errores
            }

            // Opciones comunes por si no encuentra ninguna
            AgregarSiNoExiste(lista, ".");
            AgregarSiNoExiste(lista, "(local)");
            AgregarSiNoExiste(lista, "localhost");
            AgregarSiNoExiste(lista, ".\\SQLEXPRESS");
            AgregarSiNoExiste(lista, Environment.MachineName);
            AgregarSiNoExiste(lista,
                $"{Environment.MachineName}\\SQLEXPRESS");

            return lista
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static void AgregarSiNoExiste(
            List<string> lista,
            string valor)
        {
            if (!lista.Contains(valor,
                StringComparer.OrdinalIgnoreCase))
            {
                lista.Add(valor);
            }
        }
    }
}