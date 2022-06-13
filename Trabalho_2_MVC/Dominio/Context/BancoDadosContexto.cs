using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Context.Map;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Interfaces.Context;

namespace Trabalho_2_MVC.Dominio.Context
{
    public class BancoDadosContexto : DbContext, IDbContext
    {

        public BancoDadosContexto() : base("BancoDadosContexto")
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new ClienteMap());
            modelBuilder.Configurations.Add(new FormaPagamentoMap());
            modelBuilder.Configurations.Add(new OrdemServicoMap());
            modelBuilder.Configurations.Add(new PagamentoMap());
            modelBuilder.Configurations.Add(new ServicoMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
        }

    }
}