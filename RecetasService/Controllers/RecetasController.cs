using System;
using System.Linq;
using System.Net;
using System.Web.Http;

public class RecetasController : ApiController
{
    private readonly RecetasDbContext _context = new RecetasDbContext();

    // GET: api/recetas
    [HttpGet]
    public IHttpActionResult GetRecetas()
    {
        var recetas = _context.Recetas.ToList();
        return Ok(recetas);
    }

    // GET: api/recetas/{codigoUnico}
    [HttpGet]
    public IHttpActionResult GetRecetaByCodigo(string CitaId)
    {
        var receta = _context.Recetas.FirstOrDefault(r => r.CitaId == CitaId);
        if (receta == null)
        {
            return NotFound();
        }
        return Ok(receta);
    }

    // POST: api/recetas
    [HttpPost]
    public IHttpActionResult PostReceta(Receta receta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        receta.CitaId = Guid.NewGuid().ToString();
        receta.FechaCreacion = DateTime.Now;
        _context.Recetas.Add(receta);
        _context.SaveChanges();
        return CreatedAtRoute("DefaultApi", new { id = receta.Id }, receta);
    }

    // PUT: api/recetas/{id}
    [HttpPut]
    public IHttpActionResult PutReceta(int id, Receta receta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingReceta = _context.Recetas.Find(id);
        if (existingReceta == null)
        {
            return NotFound();
        }

        existingReceta.Descripcion = receta.Descripcion;
        existingReceta.Estado = receta.Estado;
        _context.SaveChanges();
        return StatusCode(HttpStatusCode.NoContent);
    }

    // DELETE: api/recetas/{id}
    [HttpDelete]
    public IHttpActionResult DeleteReceta(int id)
    {
        var receta = _context.Recetas.Find(id);
        if (receta == null)
        {
            return NotFound();
        }

        _context.Recetas.Remove(receta);
        _context.SaveChanges();
        return Ok(receta);
    }
    // GET: api/recetas/paciente/{pacienteNombre}
    [HttpGet]
    [Route("api/recetas/paciente/{pacienteNombre}")]
    public IHttpActionResult GetRecetasByPaciente(string pacienteNombre)
    {
        var recetas = _context.Recetas.Where(r => r.Paciente == pacienteNombre).ToList();
        if (recetas == null || !recetas.Any())
        {
            return NotFound();
        }
        return Ok(recetas);
    }

    // PUT: api/recetas/estado/{id}
    [HttpPut]
    [Route("api/recetas/estado/{id}")]
    public IHttpActionResult UpdateRecetaEstado(int id, string estado)
    {
        var receta = _context.Recetas.Find(id);
        if (receta == null)
        {
            return NotFound();
        }

        receta.Estado = estado;
        _context.SaveChanges();
        return StatusCode(HttpStatusCode.NoContent);
    }
}
