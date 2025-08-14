using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuSaludMental.Web.Data;
using TuSaludMental.Web.Models;

namespace TuSaludMental.Web.Controllers
{
    public class CitasController : Controller
    {
        private readonly ApplicationDbContext _db;
        private const string DEMO_USER = "DEMO"; // sin auth por ahora

        public CitasController(ApplicationDbContext db) => _db = db;

        // LISTAR
        public async Task<IActionResult> Index()
        {
            var paciente = await GetOrCreatePacienteAsync();
            var citas = await _db.Citas
                .Where(c => c.PacienteId == paciente.Id)
                .OrderByDescending(c => c.FechaHora)
                .ToListAsync();
            return View(citas);
        }

        // GET: crear
        public async Task<IActionResult> Crear()
        {
            // Si no hay seguro activo, avisar en la vista
            ViewBag.TieneSeguroActivo = await TieneSeguroVigenteAsync();
            return View(new Appointment { FechaHora = DateTime.Now.AddDays(1) });
        }

        // POST: crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Appointment model)
        {
            var paciente = await GetOrCreatePacienteAsync();

            // Validar seguro
            var tieneSeguro = await TieneSeguroVigenteAsync();
            if (!tieneSeguro)
            {
                ModelState.AddModelError(string.Empty, "Debe tener un seguro ACTIVO y vigente para crear citas.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.TieneSeguroActivo = tieneSeguro;
                return View(model);
            }

            model.PacienteId = paciente.Id;
            model.Estado = "PENDIENTE";
            _db.Citas.Add(model);
            await _db.SaveChangesAsync();

            // programar recordatorio 24h antes (simple, sin worker todavía)
            _db.Recordatorios.Add(new Reminder
            {
                CitaId = model.Id,
                Canal = "EMAIL",
                EnviarEl = model.FechaHora.AddHours(-24)
            });
            await _db.SaveChangesAsync();

            TempData["ok"] = "Cita creada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // POST: cancelar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(int id)
        {
            var cita = await _db.Citas.FindAsync(id);
            if (cita != null)
            {
                cita.Estado = "CANCELADA";
                await _db.SaveChangesAsync();
                TempData["ok"] = "Cita cancelada.";
            }
            return RedirectToAction(nameof(Index));
        }

        // Completar/editar perfil del paciente DEMO (tel, sangre, perfil)
        public async Task<IActionResult> CompletarPerfil()
        {
            var p = await GetOrCreatePacienteAsync();
            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompletarPerfil(Patient p)
        {
            if (p.Id == 0) _db.Pacientes.Add(p);
            else _db.Pacientes.Update(p);

            await _db.SaveChangesAsync();
            TempData["ok"] = "Perfil guardado.";
            return RedirectToAction(nameof(Index));
        }

        // ===== Helpers =====
        private async Task<Patient> GetOrCreatePacienteAsync()
        {
            var p = await _db.Pacientes.FirstOrDefaultAsync(x => x.UserId == DEMO_USER);
            if (p == null)
            {
                p = new Patient { UserId = DEMO_USER, Perfil = "ADULTO" };
                _db.Pacientes.Add(p);
                await _db.SaveChangesAsync();
            }
            return p;
        }

        private async Task<bool> TieneSeguroVigenteAsync()
        {
            var paciente = await GetOrCreatePacienteAsync();
            var hoy = DateOnly.FromDateTime(DateTime.Today);
            return await _db.PacienteSeguros
                .AnyAsync(s => s.PacienteId == paciente.Id &&
                               s.Estado == "ACTIVO" &&
                               s.VigenteHasta >= hoy);
        }
    }
}
