using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace ReseniasProyect.Models.dominio
{
    public class Resenia
    {
        public int Id { get; set; }
        [DisplayName("Nombre Articulo")]
        public int ArticuloId { get; set; }
        public Articulo? Articulo { get; set; }
        public int Puntuacion { get; set; }
        public string comentario { get; set; }
        public DateTime DateTime { get; set; }
        // El ID del usuario (clave foránea)
        public string UserId { get; set; }

        // La propiedad de navegación
        public IdentityUser? User { get; set; }
    }
}
