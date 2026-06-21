using System;
using System.Collections.Generic;
using System.Text;

namespace Servicios
{
    public interface IObserved
    {
        void NotificarObservadores();
        void AgregarObservador(IObserver observable);
        void EliminarObservador(IObserver observable);
    }
}
