using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuSaludMental.Web.Data;
using TuSaludMental.Web.Models;

namespace TuSaludMental.Web.Controllers
{
    public class SegurosController : Controller
    {
        private readonly ApplicationDbContext _db;
        private const string DEMO_USER = "DEMO";

        public SegurosController(ApplicationDbContext db) => _db = db;

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

        // Listado + formulario de alta
        public async Task<IActionResult> MisSeguros()
        {
            var paciente = await GetOrCreatePacienteAsync();

            var lista = await _db.PacienteSeguros
                .Include(x => x.Seguro)
                .Where(x => x.PacienteId == paciente.Id)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            ViewBag.Seguros = await _db.Seguros.OrderBy(s => s.Nombre).ToListAsync();
            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(int seguroId, string numeroPoliza, DateOnly vigenteHasta)
        {
            var paciente = await GetOrCreatePacienteAsync();

            _db.PacienteSeguros.Add(new PatientInsurance
            {
                PacienteId = paciente.Id,
                SeguroId = seguroId,
                NumeroPoliza = numeroPoliza,
                VigenteHasta = vigenteHasta,
                Estado = "ACTIVO"
            });

            await _db.SaveChangesAsync();
            TempData["ok"] = "Seguro agregado.";
            return RedirectToAction(nameof(MisSeguros));
        }

        // Verificar por número de póliza (opcional)
        public async Task<IActionResult> Verificar(string? numero)
        {
            PatientInsurance? poliza = null;
            if (!string.IsNullOrWhiteSpace(numero))
            {
                poliza = await _db.PacienteSeguros
                    .Include(p => p.Seguro)
                    .FirstOrDefaultAsync(p => p.NumeroPoliza == numero);
            }
            return View(poliza);
        }
    }
}

