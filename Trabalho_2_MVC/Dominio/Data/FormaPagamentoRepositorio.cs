using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Interfaces.Context;

namespace Trabalho_2_MVC.Dominio.Data
{
    public class FormaPagamentoRepositorio : RepositorioBase<FormaPagamento>
    {
        public FormaPagamentoRepositorio(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}