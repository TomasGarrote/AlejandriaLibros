using BE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Servicios
{
    public class SerializacionService
    {
        public void SerializarLibros(List<Libro> libros, string rutaArchivo)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Libro>));
            using (TextWriter writer = new StreamWriter(rutaArchivo))
            {
                serializer.Serialize(writer, libros);
            }
        }
        public List<Libro> DeserializarLibros(string rutaArchivo)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Libro>));
            using (TextReader reader = new StreamReader(rutaArchivo))
            {
                return (List<Libro>)serializer.Deserialize(reader);
            }
        }
    }
}
