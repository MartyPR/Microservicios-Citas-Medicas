using System;
public class Cita
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Paciente { get; set; }
    public string Medico { get; set; }
    public string Especialidad { get; set; }
    public string Estado { get; set; } // "Pendiente", "En proceso", "Finalizada"
}
