using Microsoft.EntityFrameworkCore;
using PersonasMicroservice.Models;
public class PersonasDbContext : DbContext
{
    public DbSet<Persona> Personas { get; set; }

    public PersonasDbContext(DbContextOptions<PersonasDbContext> options) : base(options) { }
}
