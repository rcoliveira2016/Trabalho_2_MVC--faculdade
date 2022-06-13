using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Interfaces.Context;
using Trabalho_2_MVC.Dominio.Interfaces.Data;

namespace Trabalho_2_MVC.Dominio.Data
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuariosRepositorio
    {
        public UsuarioRepositorio(IDbContext dbContext) : base(dbContext)
        {
        }

        public Usuario BuscarPorLogin(string login)
        {
            return ListaTodos().FirstOrDefault(x => x.Login == login);
        }

        public bool ExisteLogin(string login)
        {
            return ListaTodos().Any(x => x.Login == login);
        }
    }
}