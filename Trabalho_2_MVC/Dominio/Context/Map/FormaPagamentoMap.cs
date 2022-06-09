using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Entidades;

namespace Trabalho_2_MVC.Dominio.Context.Map
{
    public class FormaPagamentoMap : EntityTypeConfiguration<FormaPagamento>
    {
        public FormaPagamentoMap()
        {
            ToTable("FormaPagamento");

            HasKey(x => x.Id);
            Property(x => x.CodigoBarra);
            Property(x => x.CodigoPix);
            Property(x => x.CodigoSegurança);
            Property(x => x.NumeroCartão);            
        }
    }
}