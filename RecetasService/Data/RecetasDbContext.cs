using System.Collections.Generic;
using System.Data.Entity;

public class RecetasDbContext : DbContext
{
    public DbSet<Receta> Recetas { get; set; }

    public RecetasDbContext() : base("RecetasDbContext")
    {
    }
}
