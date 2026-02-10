using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReseniasProyect.Models.dominio;

namespace ReseniasProyect.Data
{
    public class ReseniasDbContex : IdentityDbContext
    {
        public ReseniasDbContex(DbContextOptions options) : base(options) { }

        public DbSet<Articulo>  Articulos { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Resenia> resenias { get; set; }
    }
}
