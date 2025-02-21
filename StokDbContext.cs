using Microsoft.EntityFrameworkCore;
using son_atik_takip.Models;

namespace son_atik_takip.Data
{
    public class StokDbContext : DbContext
    {
        public StokDbContext(DbContextOptions<StokDbContext> options)
            : base(options)
        {
        }

        // "Stoklar" adında tablo oluşturulacak
        public DbSet<Stok> Stoklar { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=stok.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stok>().HasData(
                new Stok
                {
                    Id = 1,
                    Plaka = "34ABC123", // ZORUNLU ALAN
                    Miktar = 100,
                    Tedarikci= "ALİ",
                    Urun= "Kağıt"
                }
            );
        }
        
     
    }
}
