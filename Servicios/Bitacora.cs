namespace Servicios
{
    public class Bitacora
    {
        public int Id_Evento { get; set; }
        public string Login { get; set; }
        public DateTime Fecha { get; set; }
        public string Modulo { get; set; }
        public string Evento { get; set; }
        public int Criticidad { get; set; }

        public string FechaSolo => Fecha.ToString("dd/MM/yyyy");
        public string HoraSolo => Fecha.ToString("HH:mm");
    }
}
