using System;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Paciente> Pacientes { get; }
        Task<int> CommitAsync();
    }
}

