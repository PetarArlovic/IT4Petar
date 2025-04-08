using Microsoft.EntityFrameworkCore;
using POSApi.Domain.Models;

namespace POSApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        //Configuring DB tables
        public DbSet<Kupac> KUPAC { get; set; }
        public DbSet<Proizvod> PROIZVOD { get; set; }
        public DbSet<Zaglavlje_racuna> ZAGLAVLJE_RACUNA { get; set; }
        public DbSet<Stavke_racuna> STAVKE_RACUNA { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Kupac>()
                .HasIndex(k => k.SIFRA)
                .IsUnique();

            modelBuilder.Entity<Proizvod>()
                .HasIndex(p => p.SIFRA)
                .IsUnique();

            modelBuilder.Entity<Zaglavlje_racuna>()
                .HasIndex(z => z.BROJ)
                .IsUnique();

            modelBuilder.Entity<Zaglavlje_racuna>()
                .HasOne(z => z.KUPAC)
                .WithMany(k => k.ZAGLAVLJE_RACUNA)
                .HasForeignKey(z => z.KUPACId);

            modelBuilder.Entity<Stavke_racuna>()
                .HasOne(s => s.PROIZVOD)
                .WithMany(p => p.STAVKE_RACUNA)
                .HasForeignKey(s => s.PROIZVODId);

            modelBuilder.Entity<Stavke_racuna>()
                .HasOne(s => s.ZAGLAVLJE_RACUNA)
                .WithMany(z => z.STAVKE_RACUNA)
                .HasForeignKey(s => s.ZAGLAVLJE_RACUNAId);

        }
    }
}
