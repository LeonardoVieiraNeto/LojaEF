using LojaEF.DAO;
using LojaEF.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaEF
{
  class Program
  {
    static void Main(string[] args)
    {
      var context = new EntidadesContext();

      #region Inserindo um usuário

      Usuario victor = new Usuario { Nome = "victor dois" };
      UsuarioDAO usuDAo = new UsuarioDAO();
      usuDAo.Adiciona(victor);

      usuDAo.AtualizaNome(1, "Nome Alterado");

      #endregion

      #region Manipulando o relacionamento entre categoria e produtos

      Categoria categoria = new Categoria();
      categoria.Nome = "Informatica";
      context.Categorias.Add(categoria);
      context.SaveChanges();

      Produto produto1 = new Produto()
      {
        //Categoria = categoria,
        CategoriaID = 1,
        Nome = "Monitor",
        Descricao = "Monitor Muito Moderno",
        Preco = 200
      };
      context.Produtos.Add(produto1);

      Produto produto2 = new Produto()
      {
        Categoria = categoria,
        Nome = "Mouse",
        Descricao = "Mouse Muito Barato",
        Preco = 50
      };
      context.Produtos.Add(produto2);

      Produto produtoSemCategoria = new Produto()
      {
        Nome = "Produto Sem Categoria",
        Descricao = "Produto Sem Categoria",
        Preco = 75
      };
      context.Produtos.Add(produtoSemCategoria);

      categoria = context.Categorias.Find(1L);
      IList<Produto> produtos = categoria.Produtos;
      Console.WriteLine(string.Format("Existem {0} produtos cadastrados", produtos != null ? produtos.Count.ToString() : "0"));

      Console.WriteLine(string.Format("Categoria {0}: ", categoria.Nome));

      foreach (Produto p in produtos)
      {

        Console.WriteLine("Produto: " + p.Nome);
      }

      context.SaveChanges();

      #endregion

      #region Problema do N+1 queries

      var busca = from c in context.Categorias.Include("Produtos") select c;
      //Console.WriteLine(busca);
      //return busca.ToList();

      #endregion

      #region Trabalhando com relacionamentos many-to-many

      Produto p1 = context.Produtos.Find(1);
      Produto p2 = context.Produtos.Find(2);
      Usuario cliente = context.Usuarios.Find(1);
      Venda venda = new Venda();
      venda.Cliente = cliente;
      venda.Produtos.Add(p1);
      venda.Produtos.Add(p2);

      context.Vendas.Add(venda);
      context.SaveChanges();

      #endregion

      #region Busca a venda(relacionamento many-to-many)

      Venda vDB = context.Vendas.Find(1);

      #endregion

      #region Trabalhando com Herança

      PessoaFisica pessoaFisica= new PessoaFisica();
      pessoaFisica.Nome = "Pessoa Física 1";
      pessoaFisica.Senha = "12345";
      pessoaFisica.CPF = 07470906614;

      context.Usuarios.Add(pessoaFisica);

      PessoaJuridica pessoaJuridica = new PessoaJuridica();
      pessoaJuridica.Nome = "Pessoa Juridica 1";
      pessoaJuridica.Senha = "11313";
      pessoaJuridica.CNPJ = 07517077000182;

      context.Usuarios.Add(pessoaJuridica);

      context.SaveChanges();

      #endregion

      Console.ReadLine();
    }
  }
}
