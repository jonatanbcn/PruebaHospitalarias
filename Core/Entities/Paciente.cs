using System;
namespace Core.Entities
{
    public class Paciente
    {
        public int Nhc { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        public string Dni { get; set; }
        public string? Cip { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string? Observaciones { get; set; }
    }
}

