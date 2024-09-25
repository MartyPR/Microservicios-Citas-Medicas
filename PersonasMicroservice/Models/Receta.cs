using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonasMicroservice.Models
{
    public class Receta
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int PacienteId { get; set; }
        public string Estado { get; set; }
    }
}