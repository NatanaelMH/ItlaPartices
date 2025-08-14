using Microsoft.EntityFrameworkCore;
using TuSaludMental.Web.Models;

namespace TuSaludMental.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Patient> Pacientes => Set<Patient>();
        public DbSet<Insurance> Seguros => Set<Insurance>();
        public DbSet<PatientInsurance> PacienteSeguros => Set<PatientInsurance>();
        public DbSet<Appointment> Citas => Set<Appointment>();
        public DbSet<Reminder> Recordatorios => Set<Reminder>();
        public DbSet<Article> Articulos => Set<Article>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

            b.Entity<Patient>().HasIndex(x => x.UserId).IsUnique();

            b.Entity<PatientInsurance>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(x => x.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Entity<PatientInsurance>()
                .HasOne<Insurance>()
                .WithMany()
                .HasForeignKey(x => x.SeguroId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Entity<Appointment>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(a => a.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
