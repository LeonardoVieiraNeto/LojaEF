using LojaEF.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaEF.DAO
{
  public class UsuarioDAO
  {
    private EntidadesContext contexto;

    public UsuarioDAO()
    {
      this.contexto = new EntidadesContext();
      this.contexto.Database.CreateIfNotExists();
    }

    public void Adiciona(Usuario usuario)
    {
      contexto.Usuarios.Add(usuario);
      contexto.SaveChanges();
    }

    public Usuario BuscaPorId(int id)
    {
      return contexto.Usuarios.Find(id);
    }

    public void Remove(Usuario usuario)
    {
      contexto.Usuarios.Remove(usuario);
      contexto.SaveChanges();
    }

    public void AtualizaNome(int id, string novoNome)
    {
      Usuario usu = BuscaPorId(id);
      usu.Nome = novoNome;
      contexto.SaveChanges();
    }
  }
}
