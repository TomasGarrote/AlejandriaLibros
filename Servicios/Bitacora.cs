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
        public string DVH { get; set; }

        public string FechaSolo => Fecha.ToString("dd/MM/yyyy");
        public string HoraSolo => Fecha.ToString("HH:mm");

        public Bitacora()
        {
            
        }
        public Bitacora(int idEvento, string login, DateTime fecha, string modulo, string evento, int criticidad, string dvh)
        {
            this.Id_Evento = idEvento;
            this.Login = login;
            this.Fecha = fecha;
            this.Modulo = modulo;
            this.Evento = evento;
            this.Criticidad = criticidad;
            this.DVH = dvh;
        }
    }
}
