using System;
using Core.Entities;
using Core.Interfaces;

namespace Services.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PacienteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IEnumerable<Paciente>> GetAllPacientes()
        {
            if (_unitOfWork == null || _unitOfWork.Pacientes == null)
            {
                throw new InvalidOperationException("UnitOfWork or Pacientes repository is not initialized");
            }

            return await _unitOfWork.Pacientes.GetAllAsync() ?? Enumerable.Empty<Paciente>();
        }

        public async Task<Paciente> GetPacienteByNhc(int nhc)
        {
            var paciente = await _unitOfWork.Pacientes.GetByIdAsync(nhc);
            return paciente ?? throw new KeyNotFoundException($"Paciente con NHC {nhc} no encontrado");
        }

        public async Task<Paciente> GetPacienteByDni(string dni)
        {
            var pacientes = await _unitOfWork.Pacientes.GetAllAsync();
            return pacientes.FirstOrDefault(p => p.Dni == dni);
        }

        public async Task AddPaciente(Paciente paciente)
        {
            if (paciente == null) throw new ArgumentNullException();

            // Validar DNI único
            var existing = await GetPacienteByDni(paciente.Dni);
            if (existing != null)
            {
                throw new InvalidOperationException("Ya existe un paciente con este DNI");
            }

            await _unitOfWork.Pacientes.AddAsync(paciente);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdatePaciente(Paciente paciente)
        {
            // Validar DNI único (excluyendo el paciente actual)
            var existing = await GetPacienteByDni(paciente.Dni);
            if (existing != null && existing.Nhc != paciente.Nhc)
            {
                throw new InvalidOperationException("Ya existe otro paciente con este DNI");
            }

            _unitOfWork.Pacientes.Update(paciente);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeletePaciente(int nhc)
        {
            var paciente = await _unitOfWork.Pacientes.GetByIdAsync(nhc);
            if (paciente != null)
            {
                _unitOfWork.Pacientes.Delete(paciente);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Paciente>> SearchPacientes(string searchTerm)
        {
            var pacientes = await _unitOfWork.Pacientes.GetAllAsync();
            return (IEnumerable<Paciente>)pacientes.Where(p =>
                p.Nombre.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Apellido1.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Apellido2 != null && p.Apellido2.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Dni.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Cip != null && p.Cip.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }
    }

}

