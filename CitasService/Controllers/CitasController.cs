using CitasService.Data;
using CitasService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace CitasService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly CitasDbContext _context;
        private readonly RabbitMQSender _rabbitMQSender;

        public CitasController(CitasDbContext context, RabbitMQSender rabbitMQSender)
        {
            _context = context;
            _rabbitMQSender = rabbitMQSender;
        }

        // Obtener todas las citas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cita>>> GetCitas()
        {
            return await _context.Citas.ToListAsync();
        }

        // Obtener una cita por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetCitaById(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
                return NotFound();

            return cita;
        }

        // Crear una nueva cita
        [HttpPost]
        public async Task<ActionResult<Cita>> CrearCita(Cita cita)
        {
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCitaById), new { id = cita.Id }, cita);
        }

        // Actualizar el estado de una cita
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCita(int id, Cita citaActualizada)
        {
            if (id != citaActualizada.Id)
                return BadRequest();

            _context.Entry(citaActualizada).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Si la cita está finalizada, enviar un mensaje a RabbitMQ
            if (citaActualizada.Estado == "Finalizada")
            {
                _rabbitMQSender.EnviarMensaje($"Cita con ID {id} finalizada.");
            }

            return NoContent();
        }

        // Eliminar una cita
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCita(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
                return NotFound();

            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
