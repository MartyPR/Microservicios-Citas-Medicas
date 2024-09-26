using CitasService.Models;
using Microsoft.EntityFrameworkCore;

namespace CitasService.Data
{
    public class CitasDbContext : DbContext
    {
        public DbSet<Cita> Citas { get; set; } // Tabla de citas.

        public CitasDbContext(DbContextOptions<CitasDbContext> options) : base(options) { }
    }
}
