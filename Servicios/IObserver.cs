using System;
using System.Collections.Generic;
using System.Text;

namespace Servicios
{
    public interface IObserver
    {
        void Actualizar(LanguageManager lenguaje);
    }
}
