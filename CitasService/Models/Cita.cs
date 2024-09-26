using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasService.Models
{
    public class Cita
    {
        public int Id { get; set; } // ID de la cita, llave primaria.
        public DateTime Fecha { get; set; } // Fecha de la cita.
        public string Lugar { get; set; } // Lugar donde se realizará la cita.
        public int PacienteId { get; set; } // ID del paciente.
        public int MedicoId { get; set; } // ID del médico.
        public string Estado { get; set; } // "Pendiente", "En proceso", "Finalizada".
    }
}