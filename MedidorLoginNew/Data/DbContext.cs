using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedidorLoginNew.Data
{
    public class MedidorDbContext : IdentityDbContext
    {
        public MedidorDbContext(DbContextOptions<MedidorDbContext> options)
            : base(options)
        {
        }
    }
}
