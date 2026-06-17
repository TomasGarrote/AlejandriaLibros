using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using Newtonsoft.Json;

namespace Servicios
{
    internal class LanguageManager:IObserved
    {
        private List<IObserver> _observers;                                                                                                                           //private ResourceManager _manager;
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


        private void CargarJsonIdioma()
        {
            try
            {
                string jsonget = File.ReadAllText(GetJsonRute("ConfigLenguage"));
                _language = JsonConvert.DeserializeObject<DefaultLenguage>(jsonget);

                string readjson = File.ReadAllText(GetJsonRute(_language.Idioma));

                _jsonidioma = JsonConvert.DeserializeObject<Dictionary<string, string>>(readjson);

                NotificarObservadores();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void CambiarIdiomaPredeterminado(string culturename = "es")
        {

            string jsonruta = GetJsonRute("ConfigLenguage");

            //No hace falta validar si el archivo EXISTE porque de todas formas lo va a crear SI NO EXISTE
            var defaultlenguage = new DefaultLenguage();
            defaultlenguage.Idioma = culturename;
            //Escribe el archivo con el nuevo idioma
            string jsonserialize = JsonConvert.SerializeObject(defaultlenguage, Formatting.Indented);
            File.WriteAllText(jsonruta, jsonserialize);

            CargarJsonIdioma();



        }
        public void CargarIdiomaPredeterminado()
        {
            try
            {

                if (File.Exists(GetJsonRute("ConfigLenguage")))
                {

                    CargarJsonIdioma();
                }
                else
                {
                    CambiarIdiomaPredeterminado("es");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public CultureInfo GetCurrentLanguage()
        {
            try
            {
                if (_language == null) throw new Exception(LanguageManager.Instance.GetTraduction("NoSeHaCargadoElIdiomaPredeterminadoLM"));
                return new CultureInfo(_language.Idioma);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string GetTraduction(string key)
        {
            try
            {
                if (_jsonidioma == null) throw new Exception(LanguageManager.Instance.GetTraduction("NoEstaIniciadoElIdiomaLM"));
                if (!_jsonidioma.Any(x => x.Key == key)) return string.Empty;
                return _jsonidioma[key];

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private string GetJsonRute(string name)
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            string jsonruta = Path.Combine(directory, "..", "..", "..", "..", "Security and Services", "Idiomas", $"{name}.json");
            return jsonruta;

        }
    } 
    }
