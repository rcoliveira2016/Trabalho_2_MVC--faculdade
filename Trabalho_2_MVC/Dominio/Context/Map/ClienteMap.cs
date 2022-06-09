using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Entidades;

namespace Trabalho_2_MVC.Dominio.Context.Map
{
    public class ClienteMap : EntityTypeConfiguration<Cliente>
    {
        public ClienteMap()
        {
            ToTable("Cliente");
            HasKey(x => x.Id);
            Property(x => x.CPF);
            Property(x => x.Endereco);
            Property(x => x.Nome);
            Property(x => x.Telefone);
        }
    }
}