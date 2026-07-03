
using System.Text;

namespace LibreriasExternas
{
    public class AnalizadorEstructurasPlanas
    {
        public string RenderizarMatrizDeAccesos(Dictionary<string, string> coleccionPlana)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== REPORTE DE AUDITORÍA DE PERMISOS (ADAPTEE) ===");

            foreach (var item in coleccionPlana)
            {
                sb.AppendLine($"Ruta: {item.Key} | Tipo de Acceso: {item.Value}");
            }

            return sb.ToString();
        }
    }
}