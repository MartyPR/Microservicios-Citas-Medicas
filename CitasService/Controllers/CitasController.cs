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

    // GET: api/citas/{id}
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
        return CreatedAtRoute("DefaultApi", new { id = cita.Id }, cita);
    }

    // PUT: api/citas/{id}
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

    // DELETE: api/citas/{id}
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

    // PUT: api/citas/finalizar/{id}
    [HttpPut]
    [Route("api/citas/finalizar/{id}")]
    public IHttpActionResult FinalizarCita(int id)
    {
        var cita = _context.Citas.Find(id);
        if (cita == null)
        {
            return NotFound();
        }

        cita.Estado = "Finalizada";
        _context.SaveChanges();

        // enviar mensaje a RecetaQueue
        RabbitMQHelper.SendMessage("RecetaQueue", $"Cita finalizada para paciente {cita.Paciente}. Médico: {cita.Medico}");

        return Ok(cita);
    }
}
