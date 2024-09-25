using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonasMicroservice.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string TipoPersona { get; set; } 
    }
}