using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonasMicroservice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PersonasController : ControllerBase
{
    private readonly PersonasDbContext _context;

    public PersonasController(PersonasDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
    {
        return await _context.Personas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Persona>> GetPersonaById(int id)
    {
        var persona = await _context.Personas.FindAsync(id);
        if (persona == null)
            return NotFound();
        return persona;
    }

    [HttpPost]
    public async Task<ActionResult<Persona>> CreatePersona(Persona persona)
    {
        _context.Personas.Add(persona);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPersonaById), new { id = persona.Id }, persona);
    }
}
