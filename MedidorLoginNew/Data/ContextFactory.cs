using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MedidorLoginNew.Data
{
    public class ContextFactory : IDesignTimeDbContextFactory<MedidorDbContext>
    {
        public MedidorDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MedidorDbContext>();
            optionsBuilder.UseSqlite("Data Source=medidor.db");

            return new MedidorDbContext(optionsBuilder.Options);
        }
    }
}
