using System;
using System.Collections.Generic;
using System.Text;

namespace Servicios
{
    internal interface IObserver
    {
        void Actualizar(LanguageManager lenguaje);
    }
}
