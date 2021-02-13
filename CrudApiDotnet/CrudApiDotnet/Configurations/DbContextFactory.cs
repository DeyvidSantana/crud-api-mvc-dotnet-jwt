using CrudApiDotnet.Controllers;
using CrudApiDotnet.Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CrudApiDotnet.Configurations
{
    public class DbContextFactory : IDesignTimeDbContextFactory<CursoDbContext>
    {
        public CursoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>()
                .UseSqlServer(@"Server = localhost; Database = CrudApiDotnet; Trusted_Connection = True;");
            var contexto = new CursoDbContext(optionsBuilder.Options);

            return contexto;
        }
    }
}
