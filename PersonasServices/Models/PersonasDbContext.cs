using System.Data.Entity;

public class PersonasDbContext : DbContext
{
    public DbSet<Persona> Personas { get; set; }

    public PersonasDbContext() : base("PersonasDbContext")
    {
    }
}