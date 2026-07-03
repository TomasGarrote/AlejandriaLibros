using LibreriasExternas;
using Servicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servicios
{
    public class PermisosEstructuraAdapter : IExportadorPermisos
    {
        private readonly AnalizadorEstructurasPlanas _analizadorExterno;

        public PermisosEstructuraAdapter(AnalizadorEstructurasPlanas analizadorExterno)
        {
            _analizadorExterno = analizadorExterno;
        }

        public string ExportarReporte(Familia familiaRaiz)
        {
            var mapaPlano = new Dictionary<string, string>();

            ProcesarNodoRecursivo(familiaRaiz, "Raiz", mapaPlano);

            return _analizadorExterno.RenderizarMatrizDeAccesos(mapaPlano);
        }

        private void ProcesarNodoRecursivo(ComponentePermiso nodo, string rutaActual, Dictionary<string, string> mapa)
        {
            string nuevaRuta = $"{rutaActual} > {nodo.Nombre}";

            if (nodo is PermisoSimple)
            {
                mapa.Add(nuevaRuta, "PERMISO_SIMPLE (HOJA)");
            }
            else if (nodo is Familia)
            {
                mapa.Add(nuevaRuta, "FAMILIA (CONTENEDOR)");
                foreach (var hijo in nodo.ObtenerHijos())
                {
                    ProcesarNodoRecursivo(hijo, nuevaRuta, mapa);
                }
            }
        }
    }
}
