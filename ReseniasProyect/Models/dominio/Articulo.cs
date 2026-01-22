namespace ReseniasProyect.Models.dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CategoriaId { get; set; } //este campo ya esta generado en la db.
        public Categoria? Categoria { get; set; } //este campo servira como navegador. "?" para hacerla opcional
        public float precio { get; set; }

    }
}
