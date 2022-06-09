using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Entidades;

namespace Trabalho_2_MVC.Dominio.Context.Map
{
    public class PagamentoMap : EntityTypeConfiguration<Pagamento>
    {
        public PagamentoMap()
        {
            ToTable("Pagamento");

            HasKey(x => x.Id);

            HasRequired(x => x.FormaPagamento)
                .WithRequiredPrincipal(x => x.Pagamento);
            
        }
    }
}