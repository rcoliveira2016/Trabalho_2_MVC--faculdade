using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Entidades;

namespace Trabalho_2_MVC.Dominio.Context.Map
{
    public class OrdemServicoMap : EntityTypeConfiguration<OrdemServico>
    {
        public OrdemServicoMap()
        {
            ToTable("OrdemServico");

            HasKey(x => x.Id);

            HasRequired(c => c.Cliente)
                .WithMany(x => x.OrdensServicos)
                .HasForeignKey(p => p.IdCliente);

            HasRequired(c => c.Servico)
                .WithMany(x => x.OrdensServicos)
                .HasForeignKey(p => p.IdServico);

            HasRequired(c => c.Usuario)
                .WithMany(x => x.OrdensServicos)
                .HasForeignKey(p => p.IdUsuario);

            HasRequired(c => c.Pagamento)
                .WithRequiredPrincipal(x => x.OrdemServico);
        }
    }
}