using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuSaludMental.Web.Data;
using TuSaludMental.Web.Models;

namespace TuSaludMental.Web.Controllers
{
    public class ArticulosController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ArticulosController(ApplicationDbContext db) => _db = db;

        // GET: /Articulos
        public async Task<IActionResult> Index(string? q)
        {
            var query = _db.Articulos.AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(a => a.Titulo.Contains(q) || (a.Tags ?? "").Contains(q));
            var data = await query.OrderByDescending(a => a.CreatedAt).ToListAsync();
            ViewBag.Q = q;
            return View(data);
        }

        // GET: /Articulos/Crear
        public IActionResult Crear() => View(new Article());

        // POST: /Articulos/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Article m)
        {
            if (!ModelState.IsValid) return View(m);
            m.CreatedAt = DateTime.UtcNow;
            _db.Articulos.Add(m);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Articulos/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var item = await _db.Articulos.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /Articulos/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Article m)
        {
            if (id != m.Id) return NotFound();
            if (!ModelState.IsValid) return View(m);
            _db.Articulos.Update(m);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Articulos/Detalle/5
        public async Task<IActionResult> Detalle(int id)
        {
            var item = await _db.Articulos.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /Articulos/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var item = await _db.Articulos.FindAsync(id);
            if (item != null)
            {
                _db.Articulos.Remove(item);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
