using IBoxs.Model;
using System.Data.Entity;

namespace IBoxs.Data.Mapping
{
    public class Mapping
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarrinhoCompra>()
                .ToTable("CarrinhoCompra")
                .HasKey(k => k.Indice)
                .Ignore(i => i.Imagens)
                .Property(p => p.Indice).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<CarrinhoCompra>()
                .HasRequired(r => r.Loja).WithMany().HasForeignKey(f => f.IdLoja).WillCascadeOnDelete(false);
            modelBuilder.Entity<CarrinhoCompra>()
               .HasRequired(r => r.ProdutoGrade).WithMany().HasForeignKey(f => f.IdProdutoGrade).WillCascadeOnDelete(false);
            modelBuilder.Entity<CarrinhoCompra>()
                .HasRequired(r => r.Cliente).WithMany().HasForeignKey(f => f.IdCliente).WillCascadeOnDelete(false);

            modelBuilder.Entity<Pessoa>()
                .ToTable("Pessoa")
                .HasKey(k => k.IdPessoa)
                .Property(p => p.IdPessoa).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Produto>()
                .ToTable("Produto")
                .HasKey(k => k.IdProduto)
                .Property(p => p.IdProduto).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Produto>()
                .HasRequired(r => r.Loja).WithMany().HasForeignKey(f => f.IdLoja).WillCascadeOnDelete(false);

            modelBuilder.Entity<Loja>()
                .ToTable("Loja")
                .HasKey(k => k.IdPessoa)
                .Property(p => p.IdPessoa).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Cliente>()
                .ToTable("Cliente")
                .HasKey(k => k.IdPessoa)
                .Property(p => p.IdPessoa).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ProdutoGrade>()
                .ToTable("ProdutoGrade")
                .HasKey(k => k.IdProdutoGrade)
                .Property(p => p.IdProdutoGrade).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ProdutoGrade>()
                .HasRequired(r => r.Produto).WithMany().HasForeignKey(f => f.IdProduto).WillCascadeOnDelete(false);
            modelBuilder.Entity<ProdutoGrade>()
                .HasRequired(r => r.Cor).WithMany().HasForeignKey(f => f.IdCor).WillCascadeOnDelete(false);
            modelBuilder.Entity<ProdutoGrade>()
                .HasRequired(r => r.Tamanho).WithMany().HasForeignKey(f => f.IdTamanho).WillCascadeOnDelete(false);

            modelBuilder.Entity<ProdutoFoto>()
                .ToTable("ProdutoFoto")
                .HasKey(k => k.IdProdutoFoto)
                .Property(p => p.IdProdutoFoto).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ProdutoFoto>()
                .HasRequired(r => r.Produto).WithMany().HasForeignKey(f => f.IdProduto).WillCascadeOnDelete(false);

            modelBuilder.Entity<Cor>()
                .ToTable("Cor")
                .HasKey(k => k.IdCor)
                .Property(p => p.IdCor).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Tamanho>()
                .ToTable("Tamanho")
                .HasKey(k => k.IdTamanho)
                .Property(p => p.IdTamanho).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Campanha>()
                .ToTable("Campanha")
                .HasKey(k => k.IdCampanha)
                .Property(p => p.IdCampanha).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<CampanhaProduto>()
                .ToTable("CampanhaProduto")
                .HasKey(k => k.IdCampanhaProduto)
                .Property(p => p.IdCampanhaProduto).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<CampanhaProduto>()
                .HasRequired(r => r.Produto).WithMany().HasForeignKey(f => f.IdProduto).WillCascadeOnDelete(false);
            modelBuilder.Entity<CampanhaProduto>()
                .HasRequired(r => r.Campanha).WithMany().HasForeignKey(f => f.IdCampanha).WillCascadeOnDelete(false);
        }
    }
}
