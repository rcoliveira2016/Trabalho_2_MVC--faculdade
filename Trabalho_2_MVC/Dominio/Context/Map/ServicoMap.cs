using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Entidades;

namespace Trabalho_2_MVC.Dominio.Context.Map
{
    public class ServicoMap : EntityTypeConfiguration<Servico>
    {
        public ServicoMap()
        {
            ToTable("Servico");

            HasKey(x => x.Id);
            Property(x => x.Nome);
            Property(x => x.Descricao);

        }
    }
}