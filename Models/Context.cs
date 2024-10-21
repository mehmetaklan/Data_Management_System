using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HACKATHON.Models
{
    public class Context:DbContext
    {
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Grup> Gruplar { get; set; }
        public DbSet<DegerTuru> DegerTurleri { get; set; }
        public DbSet<VeriGirisi> VeriGirisleri { get; set; }
        public DbSet<Sorumlu> Sorumlular { get; set; }
        public DbSet<SorumluAtama> SorumluAtamalari { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-9HDFLL4;Database=HACKATHON;Integrated Security = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Kategoriler ve Gruplar arasında 1-N ilişkisi
            modelBuilder.Entity<Grup>()
                .HasOne(g => g.Kategori)  // Bir grup bir kategoriye ait olabilir
                .WithMany(c => c.Gruplar)  // Bir kategori birden fazla gruba sahip olabilir
                .HasForeignKey(g => g.KategoriId);

            // Gruplar ve Değer Türleri arasında 1-N ilişkisi
            modelBuilder.Entity<DegerTuru>()
                .HasOne(v => v.Grup)  // Bir değer türü bir gruba ait olabilir
                .WithMany(g => g.DegerTurleri)  // Bir grup birden fazla değer türüne sahip olabilir
                .HasForeignKey(v => v.GrupId);

            // Değer Türleri ve Veri Girişleri arasında 1-N ilişkisi
            modelBuilder.Entity<VeriGirisi>()
                .HasOne(d => d.DegerTuru)  // Bir veri girişi bir değer türüne ait olabilir
                .WithMany(v => v.VeriGirisleri)  // Bir değer türü birden fazla veri girişine sahip olabilir
                .HasForeignKey(d => d.DegerTuruId);

            // Sorumlular ve Sorumlu Atamaları arasında 1-N ilişkisi
            modelBuilder.Entity<SorumluAtama>()
                .HasOne(ra => ra.Sorumlu)  // Bir sorumlu ataması bir sorumluya ait olabilir
                .WithMany(r => r.SorumluAtamalari)  // Bir sorumlu birden fazla atamaya sahip olabilir
                .HasForeignKey(ra => ra.SorumluId);

            // Değer Türleri ve Sorumlu Atamaları arasında 1-N ilişkisi
            modelBuilder.Entity<SorumluAtama>()
                .HasOne(ra => ra.DegerTuru)  // Bir sorumlu ataması bir değer türüne ait olabilir
                .WithMany(v => v.SorumluAtamalari)  // Bir değer türü birden fazla sorumlu atamasına sahip olabilir
                .HasForeignKey(ra => ra.DegerTuruId);
        }
    }
}
