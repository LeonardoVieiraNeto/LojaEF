﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaEF.Entidades
{
  public class Produto
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }

    public int? CategoriaID { get; set; }

    public Categoria Categoria { get; set; }
  }
}
