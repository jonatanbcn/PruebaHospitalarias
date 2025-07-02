using System;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Services;

namespace Web
{
    public class PacientesController : Controller
    {
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            var pacientes = await _pacienteService.GetAllPacientes();
            return View(pacientes);
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _pacienteService.GetPacienteByNhc(id.Value);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nhc,Nombre,Apellido1,Apellido2,Dni,Cip,Edad,FechaNacimiento,Sexo,Observaciones")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _pacienteService.AddPaciente(paciente);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _pacienteService.GetPacienteByNhc(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nhc,Nombre,Apellido1,Apellido2,Dni,Cip,Edad,FechaNacimiento,Sexo,Observaciones")] Paciente paciente)
        {
            if (id != paciente.Nhc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _pacienteService.UpdatePaciente(paciente);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PacienteExists(paciente.Nhc))
                    {
                        return NotFound();
                    }
                    ModelState.AddModelError("", "El registro ha sido modificado por otro usuario. Por favor, refresque y vuelva a intentar.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al actualizar: {ex.Message}");
                }
            }
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _pacienteService.GetPacienteByNhc(id.Value);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pacienteService.DeletePaciente(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PacienteExists(int id)
        {
            var paciente = await _pacienteService.GetPacienteByNhc(id);
            return paciente != null;
        }
    }

}

