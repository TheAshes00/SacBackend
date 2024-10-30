using Microsoft.EntityFrameworkCore;
using SacBackend.Models;

namespace SacBackend.Context
{
    public class CaafiContext : DbContext
    {
        private readonly String strConectionString = "Server=sacserver.mysql.database.azure.com;Port=3306;User Id=molly;Password=archer0123.;Database=caafi;";
        //private readonly String strConectionString = "server=localhost; database=caafi; user=root; password=archer0123";
        public CaafiContext(DbContextOptions<CaafiContext> options) : base(options)
        {
        }

        public CaafiContext()
        {   
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(strConectionString, 
                ServerVersion.AutoDetect(strConectionString));

            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //                                              // (1 : n) relation from Academic entity
            //                                              // to StudentEntity entity
            modelBuilder.Entity<AcademicEntity>()
                .HasMany(e => e.StudentEntity)
                .WithOne(s => s.AcademicEntEntity)
                .HasForeignKey(e => e.intPkAcademy);

            //modelBuilder.Entity<StudentEntity>()
            //.HasOne(s => s.AcademicEntEntity)
            //.WithMany(a => a.StudentEntity)
            //.HasForeignKey(s => s.intPkAcademy);

            //                                              // (1 : n) relation from StudentEntity entity
            //                                              // to Loan entity
            modelBuilder.Entity<Student>()
                .HasMany(l => l.IcLoanEntity)
                .WithOne(s => s.StuStudent)
                .HasForeignKey(l => l.strPkStudent);

            //                                              // (1 : n) From Admin(manager who lend
            //                                              // the book to student) entity relation
            //                                              // to Loan entity
            modelBuilder.Entity<Admin>()
                .HasMany(a => a.IcLoansRegisteredEntity)
                .WithOne(l => l.AdAdminRegister)
                .HasForeignKey(l => l.intPkAdminLend);

            //                                              // (1 : n) From Admin(manager who recieves
            //                                              // the book to student) entity relation
            //                                              // to Loan entity
            modelBuilder.Entity<Admin>()
                .HasMany(a => a.IcLoansReturnedEntity)
                .WithOne(l => l.AdAdminReturn)
                .HasForeignKey(l => l.intPkAdminRecieve);

            //                                              // (1 : n) relation from Material Entity to
            //                                              // MaterialLoan entity

            modelBuilder.Entity<Material>()
                .HasMany(m => m.MaterialLoanEntity)
                .WithOne(ml => ml.MaterialEntity)
                .HasForeignKey(ml => ml.strPkMaterial);

            //                                              // (1 : n) relation from Loan Entity to
            //                                              // MaterialLoan Entity
            modelBuilder.Entity<Loan>()
                .HasMany(l => l.IcMaterialLoanEntity)
                .WithOne(ml => ml.LoanEntity)
                .HasForeignKey(ml => ml.intPkLoan);

            //                                              // (1 : n) relation from Workshop Entity to
            //                                              // TutorWorkshop Entity
            modelBuilder.Entity<Workshop>()
                .HasMany(w => w.IcTutorWorkshopEntity)
                .WithOne(tw => tw.WorkshopEntity)
                .HasForeignKey(tw => tw.intPkWorkshop);

            //                                              // (1 : n) relation from Tutor Entity to
            //                                              // TutorWorkshop Entity
            modelBuilder.Entity<Tutor>()
                .HasMany(t => t.IcTutorWorkshopEntity)
                .WithOne(tw => tw.TutorEntity)
                .HasForeignKey(tw => tw.intPkTutor);

            //                                              // (1 : n) relation from WorkshopAttendance
            //                                              // Entity to TutorWorkshop Entity
            modelBuilder.Entity<WorkshopAttendance>()
                .HasOne(wa => wa.TutorWorkshopEntity)
                .WithMany(tw => tw.IcWorkshopAttendanceEntity)
                .HasForeignKey(wa => wa.intPkTutorWorkshop);

            //                                              // (1 : n) relation from WorkshopAttendance
            //                                              // Entity to Student Entity
            modelBuilder.Entity<WorkshopAttendance>()
                .HasOne(wa => wa.StudentEntity)
                .WithMany(s => s.IcWorkshopAttendanceEntity)
                .HasForeignKey(wa => wa.strPkStudent);

            //                                              // (1 : n) relation from Student
            //                                              // Entity to Loan Entity
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.StuStudent)
                .WithMany(s => s.IcLoanEntity)
                .HasForeignKey(l => l.strPkStudent);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Workshop> Workshop { get; set; }
        public DbSet<Tutor> Tutor { get; set; }
        public DbSet<AcademicEntity> AcademicEntity { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Loan> Loan { get; set; }
        public DbSet<MaterialLoan> MaterialLoan { get; set; }
        public DbSet<TutorWorkshop> TutorWorkshop { get; set; }
        public DbSet<WorkshopAttendance> WorkshopAttendance { get; set; }
    }
}
