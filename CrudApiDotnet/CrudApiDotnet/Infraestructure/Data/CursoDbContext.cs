using CrudApiDotnet.Business.Entidades;
using CrudApiDotnet.Infraestrutura.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CrudApiDotnet.Infraestrutura.Data
{
    public class CursoDbContext : DbContext
    {
        public CursoDbContext(DbContextOptions<CursoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CursoMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuario { get; set;  }
        public DbSet<Curso> Curso { get; set; }

    }
}
