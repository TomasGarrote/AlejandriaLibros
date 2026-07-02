using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Xml;


namespace Servicios
{
    public class LanguageManager : IObserved
    {
        private List<IObserver> _observers;
        private static LanguageManager _lenguageManager;
        private DefaultLenguage _language;
        private Dictionary<string, string> _jsonidioma;

        public static LanguageManager Instance
        {
            get
            {
                if (_lenguageManager == null) _lenguageManager = new LanguageManager();
                return _lenguageManager;
            }
        }
        public string CodigoIdiomaActual
        {
            get
            {
                if (_language == null)
                    return "es";

                return _language.Idioma;
            }
        }

        private LanguageManager()
        {
            _observers = new List<IObserver>();


        }

        public void AgregarObservador(IObserver observable)
        {
            _observers.Add(observable);

        }

        public void EliminarObservador(IObserver observable)
        {
            _observers.Remove(observable);

        }

        public void NotificarObservadores()
        {
            foreach (IObserver item in _observers)
            {
                item.Actualizar(this);
            }
        }


        public CultureInfo GetCurrentLanguage()
        {
            if (_language == null)
                return new CultureInfo("es");

            return new CultureInfo(
                _language.Idioma);
        }

        public string GetTraduction(string key)
        {
            if (_jsonidioma == null)
                return key;

            if (!_jsonidioma.ContainsKey(key))
                return key;

            return _jsonidioma[key];
        }
        //    private string GetJsonRute(string name)
        //    {
        //        string directory = AppDomain.CurrentDomain.BaseDirectory;
        //        string jsonruta = Path.GetFullPath(
        //Path.Combine(
        //    directory,
        //    "..",
        //    "..",
        //    "..",
        //    "Servicios",
        //    "Idiomas",
        //    $"{name}.json"));


        //        return jsonruta;

        //    }
        private string GetJsonRute(string name)
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            string jsonruta = Path.Combine(directory, "Idiomas", $"{name}.json");
            return jsonruta;
        }
        public void CargarIdioma(string codigoIdioma)
        {
            string readjson =File.ReadAllText(GetJsonRute(codigoIdioma));

            _jsonidioma =JsonConvert.DeserializeObject<Dictionary<string, string>>(readjson);

            _language = new DefaultLenguage
            {
                Idioma = codigoIdioma
            };

            NotificarObservadores();
        }
    
    }
}
    
