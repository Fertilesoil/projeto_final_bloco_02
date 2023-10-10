using Microsoft.EntityFrameworkCore;
using projeto_final_bloco_02.Models;

namespace projeto_final_bloco_02.Data
{
    public class FarmaciaDbContext : DbContext
    {
        public FarmaciaDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().ToTable("tb_produtos");
        }

        public DbSet<Produto> Produtos { get; set; } = null!;
    }
}