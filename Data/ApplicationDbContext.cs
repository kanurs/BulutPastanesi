using Microsoft.EntityFrameworkCore;
using BulutPastanesi.Models;

namespace BulutPastanesi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<SiteAyarlari> SiteAyarlari { get; set; }
    }
}