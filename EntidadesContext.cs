using LojaEF.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaEF
{
  public class EntidadesContext : DbContext
  {
    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Produto> Produtos { get; set; }

    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<Venda> Vendas { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      var usuarioBuilder = modelBuilder.Entity<Usuario>();
      usuarioBuilder.ToTable("tbl_Usuarios");
      usuarioBuilder.Property(usuario => usuario.Nome)
                    .HasColumnName("col_nome");

      var produtoBuilder = modelBuilder.Entity<Produto>();
      produtoBuilder.ToTable("tbl_Produtos");
      produtoBuilder.Property(produto => produto.Nome).HasColumnName("col_nome");
      produtoBuilder.Property(produto => produto.Descricao).HasColumnName("col_descricao");
      produtoBuilder.Property(produto => produto.Preco).HasColumnName("col_preco");

      var categoriaBuilder = modelBuilder.Entity<Categoria>()
        .HasMany(categoria => categoria.Produtos)
        .WithOptional(produto => produto.Categoria);

      //Criando um mapeamento many-to-many
      var vendaBuilder = modelBuilder.Entity<Venda>();
      vendaBuilder.HasMany(v => v.Produtos)
        .WithMany()
        .Map(relacionamento =>
        {
          relacionamento.ToTable("venda_produtos")
            .MapLeftKey("VendaID")
            .MapRightKey("ProdutoID");
        });

      modelBuilder.Entity<PessoaFisica>().ToTable("PessoaFisica");
      modelBuilder.Entity<PessoaJuridica>().ToTable("PessoaJuridica");
    }
  }


}
