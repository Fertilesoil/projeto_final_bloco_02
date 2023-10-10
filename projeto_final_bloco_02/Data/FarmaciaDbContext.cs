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
            modelBuilder.Entity<Produto>().ToTable("tb_categorias");

            _ = modelBuilder.Entity<Produto>()
                    .HasOne(_ => _.Categoria)
                    .WithMany(c => c.Produto)
                    .HasForeignKey("CategoriaId")
                    .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Produto> Produtos { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;
    }
}