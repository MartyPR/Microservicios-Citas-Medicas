using System.Linq;
using System.Net;
using System.Web.Http;

public class CitasController : ApiController
{
    private readonly CitasDbContext _context = new CitasDbContext();

    // GET: api/citas
    [HttpGet]
    public IHttpActionResult GetCitas()
    {
        var citas = _context.Citas.ToList();
        return Ok(citas);
    }

    // GET: api/citas/5
    [HttpGet]
    public IHttpActionResult GetCita(int id)
    {
        var cita = _context.Citas.Find(id);
        if (cita == null)
        {
            return NotFound();
        }
        return Ok(cita);
    }

    // POST: api/citas
    [HttpPost]
    public IHttpActionResult PostCita(Cita cita)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Citas.Add(cita);
        _context.SaveChanges();

        // Send message to RabbitMQ to trigger receta creation
        RabbitMQHelper.SendMessage("RecetaQueue", $"Cita creada: {cita.Id}");

        return CreatedAtRoute("DefaultApi", new { id = cita.Id }, cita);
    }

    // PUT: api/citas/5
    [HttpPut]
    public IHttpActionResult PutCita(int id, Cita cita)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingCita = _context.Citas.Find(id);
        if (existingCita == null)
        {
            return NotFound();
        }

        existingCita.Fecha = cita.Fecha;
        existingCita.Especialidad = cita.Especialidad;
        existingCita.Paciente = cita.Paciente;
        existingCita.Medico = cita.Medico;
        existingCita.Estado = cita.Estado;

        _context.SaveChanges();
        return StatusCode(HttpStatusCode.NoContent);
    }

    // DELETE: api/citas/5
    [HttpDelete]
    public IHttpActionResult DeleteCita(int id)
    {
        var cita = _context.Citas.Find(id);
        if (cita == null)
        {
            return NotFound();
        }

        _context.Citas.Remove(cita);
        _context.SaveChanges();
        return Ok(cita);
    }
}
