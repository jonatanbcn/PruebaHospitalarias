using System;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Context;
using Infrastructure.Models;

namespace Infrastructure.Repositorios
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Paciente> _pacientes;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<Paciente> Pacientes
        {
            get
            {
                if (_pacientes == null)
                {
                    _pacientes ??= new Repository<Paciente>(_context);
                }
                return _pacientes;
            }
        }

        public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

