using Microsoft.EntityFrameworkCore;
using SacBackend.Models;

namespace SacBackend.Context
{
    public class Caafi2Context : DbContext
    {
        private readonly String strConectionString = "server=localhost; database=caafi2; user=root; password=archer0123";
        public Caafi2Context(DbContextOptions<Caafi2Context> options) : base(options)
        {
        }

        public Caafi2Context()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(strConectionString,
                ServerVersion.AutoDetect(strConectionString));


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrestamoMaterial>().HasNoKey();
        }

        public DbSet<Alumno> Students { get; set; }
        public DbSet<Prestamo> Loans { get; set; }
        public DbSet<PrestamoMaterial> MaterialLoans {  get; set; }
        public DbSet<Materiales> Materiales {  get; set; }
    }
}
