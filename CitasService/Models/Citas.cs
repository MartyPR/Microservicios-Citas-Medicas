using System;

public class Cita
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public int Paciente { get; set; }
    public int Medico { get; set; }
    public string Especialidad { get; set; }
    public string Estado { get; set; } = "Pendiente";
}
