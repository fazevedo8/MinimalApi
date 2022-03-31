using Microsoft.EntityFrameworkCore;
using MinimalApi.Models;

namespace MinimalApi.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        public DbSet<Produto>? Produtos { get; set; }
        public DbSet<Categoria>? Categorias { get; set; }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            //Produto
            mb.Entity<Produto>().HasKey(x => x.ProdutoId);
            mb.Entity<Produto>().Property(x => x.Nome)
                                .HasMaxLength(128)
                                .IsRequired();
            mb.Entity<Produto>().Property(x => x.Preco).HasPrecision(14, 2);


            //Categoria
            mb.Entity<Categoria>().HasKey(x => x.CategoriaId);
            mb.Entity<Categoria>().Property(x => x.Nome)
                                  .HasMaxLength(128)
                                  .IsRequired();

            mb.Entity<Categoria>().Property(x => x.Descricao)
                               .HasMaxLength(200)
                               .IsRequired();

            //relacionamento
            mb.Entity<Produto>().HasOne<Categoria>(c => c.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(c => c.CategoriaId);

        }
    }
}
