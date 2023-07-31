using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using DocAppointApi.Models;

namespace DocAppointApi.Datas
{
    public class DbContextRed: DbContext
    {
        public DbContextRed(DbContextOptions<DbContextRed> options) : base(options)
        {

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medecin> Medecins { get; set; }
        public DbSet<MalariaCuire> MalariaCuires { get; set; }
        public DbSet<Consecration> Consecrations { get; set; }
        public DbSet<Adminis> Adminis { get; set; }
        public DbSet<TraitemtP> TraitemtPs { get; set; }
        public DbSet<Specialite> Specialites { get; set; }
        public DbSet<RDVM> RDVMs { get; set; }
        public DbSet<Statut> Statuts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration de l'héritage TPT pour Medecin et Patient
            modelBuilder.Entity<Medecin>().ToTable("Medecins");
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<Adminis>().ToTable("Adminis");
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        // modelBuilder.Entity<Patient>()
        //    .HasMany(p => p.MalariaCuire)
        //    .WithOne(p => p.Patient)
        //    .HasForeignKey("malaid");

        // modelBuilder.Entity<Medecin>()
        //   .HasMany(m => m.Specialite)
        //   .WithOne(m => m.Medecin)
        //    .HasForeignKey("speid");

        //  modelBuilder.Entity<RDVM>()
        //    .HasMany(r => r.Patient)
        //    .WithOne(r => r.Patient)
        //     .HasForeignKey("PatientId", "medocId");


        // modelBuilder.Entity<Consecration>()
        //     .HasMany(c => c.Patient)
        //     .WithOne(c => c.Consecration)
        //     .HasForeignKey("PatientId", "medocId");
        //


        // }
    }
}
