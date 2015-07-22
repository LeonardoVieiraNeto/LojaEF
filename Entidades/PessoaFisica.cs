using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaEF.Entidades
{
  public class PessoaFisica : Usuario
  {
    public long CPF { get; set; }
  }
}
