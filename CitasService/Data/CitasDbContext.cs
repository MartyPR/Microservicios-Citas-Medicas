using CitasService.Models;
using Microsoft.EntityFrameworkCore;

namespace CitasService.Data
{
    public class CitasDbContext : DbContext
    {
        public DbSet<Cita> Citas { get; set; } 

        public CitasDbContext(DbContextOptions<CitasDbContext> options) : base(options) { }
    }
}