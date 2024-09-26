
using System.Data.Entity;


public class CitasDbContext : DbContext
{
    public DbSet<Cita> Citas { get; set; }

    public CitasDbContext() : base("CitasDbContext")
    {
    }
}
