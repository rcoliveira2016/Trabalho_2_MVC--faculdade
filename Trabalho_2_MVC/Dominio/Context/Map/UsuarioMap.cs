using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Entidades;

namespace Trabalho_2_MVC.Dominio.Context.Map
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("Usuario");

            HasKey(x => x.Id);
            Property(x => x.NomeCompleto);
            Property(x => x.Login);
            Property(x => x.Senha);

        }
    }
}