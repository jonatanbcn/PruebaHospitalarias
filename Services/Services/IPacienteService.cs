using System;
using Core.Entities;

namespace Services.Services
{
    public interface IPacienteService
    {
        Task<IEnumerable<Paciente>> GetAllPacientes();
        Task<Paciente> GetPacienteByNhc(int nhc);
        Task<Paciente> GetPacienteByDni(string dni);
        Task AddPaciente(Paciente paciente);
        Task UpdatePaciente(Paciente paciente);
        Task DeletePaciente(int nhc);
        Task<IEnumerable<Paciente>> SearchPacientes(string searchTerm);
    }

}

