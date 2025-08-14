using Microsoft.EntityFrameworkCore;
using TuSaludMental.Web.Models;

namespace TuSaludMental.Web.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            // Asegura que la BD existe y está actualizada
            await db.Database.MigrateAsync();

            // Si no hay seguros, insertamos algunos
            if (!await db.Seguros.AnyAsync())
            {
                db.Seguros.AddRange(
                    new Insurance { Nombre = "ARS Universal" },
                    new Insurance { Nombre = "Humano" },
                    new Insurance { Nombre = "Senasa" }
                );

                await db.SaveChangesAsync();
            }
        }
    }
}

