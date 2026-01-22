namespace ReseniasProyect.Models.dominio
{
    public class Resenia
    {
        public int Id { get; set; }
        public Articulo Articulo { get; set; }
        public int Puntuacion { get; set; }
        public string comentario { get; set; }
        public DateTime DateTime { get; set; }
    }
}
