namespace Servicios
{
    public class Bitacora
    {
        public int Id_Evento { get; set; }
        public string Login { get; set; }
        public DateTime Fecha { get; set; }
        public string Modulo { get; set; }
        public string Descripcion { get; set; }
        public int Criticidad { get; set; }
    }
}
