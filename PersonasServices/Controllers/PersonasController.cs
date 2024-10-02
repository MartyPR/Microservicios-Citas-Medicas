    using System.Linq;
using System.Net;
using System.Web.Http;

public class PersonasController : ApiController
{
    private readonly PersonasDbContext _context = new PersonasDbContext();

    // GET: api/personas
    [HttpGet]
    public IHttpActionResult GetPersonas()
    {
        var personas = _context.Personas.ToList();
        
        return Ok(personas);
    }

    // GET: api/personas/5
    [HttpGet]
    public IHttpActionResult GetPersona(int PersonaId)
    {
        var persona = _context.Personas.Find(PersonaId);
        if (persona == null)
        {
            return NotFound();
        }
        return Ok(persona);
    }

    // POST: api/personas
    [HttpPost]
    public IHttpActionResult PostPersona(Persona persona)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Personas.Add(persona);
        _context.SaveChanges();
        // enviar mensaje RabbitMQ
        RabbitMQHelper.SendMessage("PersonaQueue", $"Nueva persona creada: {persona.Nombre}");

        return CreatedAtRoute("DefaultApi", new { id = persona.PersonaId }, persona);
    }

    // PUT: api/personas/5
    [HttpPut]
    public IHttpActionResult PutPersona(int id, Persona persona)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingPersona = _context.Personas.Find(id);
        if (existingPersona == null)
        {
            return NotFound();
        }

        existingPersona.Nombre = persona.Nombre;
        existingPersona.Email = persona.Email;
        existingPersona.Telefono = persona.Telefono;
        existingPersona.TipoDePersona = persona.TipoDePersona;

        _context.SaveChanges();
        return StatusCode(HttpStatusCode.NoContent);
    }

    // DELETE: api/personas/5
    [HttpDelete]
    public IHttpActionResult DeletePersona(int id)
    {
        var persona = _context.Personas.Find(id);
        if (persona == null)
        {
            return NotFound();
        }

        _context.Personas.Remove(persona);
        _context.SaveChanges();
        return Ok(persona);
    }
}
